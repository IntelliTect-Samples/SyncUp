namespace IntelliTect.SyncUp.Data.Models;

public class Post : TenantedBase
{
    public long PostId { get; init; }

    [MaxLength(500)]
    public required string Title { get; set; }

    public required string Body { get; set; }


}