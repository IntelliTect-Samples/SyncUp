using IntelliTect.SyncUp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncUp.Data.Models;
public class Image : TenantedBase
{
    // This is the key used to access the image in the Azure Blob Storage
    public required string ImageId { get; set; }

    [Read]
    [Required]
    public string Color { get; set; } = "#808080";

    public string ImageUrl => $"{AzureBlobStorageOptions.StaticStorageUrl}/{AzureBlobStorageOptions.StaticImageContainerName}/{ImageId}";
}
