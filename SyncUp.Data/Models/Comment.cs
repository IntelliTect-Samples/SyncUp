namespace IntelliTect.SyncUp.Data.Models;

public class Comment : TenantedBase
{
    public long CommentId { get; init; }

    public required string Body { get; set; }

    public Post Post { get; set; }
}