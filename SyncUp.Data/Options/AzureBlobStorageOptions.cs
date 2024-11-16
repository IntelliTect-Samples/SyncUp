namespace IntelliTect.SyncUp.Data.Services;
public class AzureBlobStorageOptions
{
    public const string SectionName = "AzureBlobStorage";
    public required string ConnectionString { get; init; }
    public required string ImageContainerName { get; init; } = "images";
    public required string StorageUrl { get; init; } = "https://syncupdev.blob.core.windows.net";

    // Static Cheater Variables
    public static string StaticImageContainerName { get; internal set; } = "";
    public static string StaticStorageUrl { get; internal set; } = "";

    public void SetStatics()
    {
        StaticImageContainerName = ImageContainerName;
        StaticStorageUrl = StorageUrl;
    }
}
