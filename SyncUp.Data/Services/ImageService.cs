using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using ImageMagick;
using IntelliTect.SyncUp.Data;
using SyncUp.Data.Models;
using System.Net;
using File = IntelliTect.Coalesce.Models.File;

namespace IntelliTect.SyncUp.Data.Services;
public class ImageService(AppDbContext db, IOptions<AzureBlobStorageOptions> options)
{
    public async Task<Image> AddImage(string url)
    {
        byte[] bytes = [];
        using (var client = new HttpClient())
        {
            bytes = await client.GetByteArrayAsync(url);
        }

        return await AddImage(new File(bytes));
    }

    public async Task<Image> AddImage(File file)
    {
        if (file.Length == 0 || file.Content is null) throw new Exception("No image provided");
        try
        {
            var imageId = Guid.NewGuid().ToString().Replace("-", "");

            var cloudBlob = GetBlobClient(imageId);

            try
            {
                await cloudBlob.UploadAsync(file.Content, new BlobUploadOptions
                {
                    Conditions = new() { IfNoneMatch = Azure.ETag.All }
                });
            }
            catch (RequestFailedException ex)
            {
                throw new Exception("An Error occurred while trying to save the specified image to blob storage", ex);

            }

            // Reset stream position to 0 before reusing
            file.Content.Position = 0;

            var imageScan = new MagickImage(file.Content);
            var colors = imageScan.Histogram();

            string dominantColor = colors.MaxBy(kvp => kvp.Value).Key.ToHexString();

            Image image = new()
            {
                ImageId = imageId,
                Color = dominantColor
            };

            db.Images.Add(image);

            await db.SaveChangesAsync();

            return image;
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to upload image", ex);
        }
    }

    private BlobClient GetBlobClient(string relativeLocation)
    {
        var connectionString = options.Value.ConnectionString;

        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(options.Value.ImageContainerName);

        return containerClient.GetBlobClient(relativeLocation);

    }
}
