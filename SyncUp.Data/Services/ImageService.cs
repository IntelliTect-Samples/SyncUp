using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using ImageMagick;
using IntelliTect.SyncUp.Data;
using SyncUp.Data.Models;
using System.Net;

namespace IntelliTect.SyncUp.Data.Services;
[Coalesce, Service]
public class ImageService(AppDbContext db, IOptions<AzureBlobStorageOptions> options)
{
    [Coalesce]
    [Execute(SecurityPermissionLevels.AllowAuthenticated, HttpMethod = IntelliTect.Coalesce.DataAnnotations.HttpMethod.Post)]
    public async Task<ItemResult<Image>> Upload(byte[] content)
    {
        try
        {
            return await AddImage(content);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<Image> AddImage(byte[] content)
    {
        try
        {
            var imageId = Guid.NewGuid().ToString().Replace("-", "");

            var cloudBlob = GetBlobClient(imageId);

            try
            {
                await cloudBlob.UploadAsync(new MemoryStream(content), new BlobUploadOptions
                {
                    Conditions = new() { IfNoneMatch = Azure.ETag.All }
                });
            }
            catch (RequestFailedException ex)
            {
                throw new Exception("An Error occurred while trying to save the specified image to blob storage", ex);

            }
            var imageScan = new MagickImage(content);
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

    [Coalesce]
    [Execute(SecurityPermissionLevels.AllowAuthenticated, HttpMethod = IntelliTect.Coalesce.DataAnnotations.HttpMethod.Post)]
    public async Task<ItemResult<Image>> UploadFromUrl(string url)
    {
        // Download the file get a byte array
        try
        {
            return await AddImage(url);
        }
        catch
        {
            // TODO: Log this
            return $"An Error occurred while trying to download the specified image.";
        }
    }

    public async Task<Image> AddImage(string url)
    {
        using (var client = new HttpClient())
        {
            var bytes = await client.GetByteArrayAsync(url);
            return await AddImage(bytes);
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
