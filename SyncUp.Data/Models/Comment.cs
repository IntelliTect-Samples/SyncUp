using System.Text.RegularExpressions;

namespace IntelliTect.SyncUp.Data.Models;

public class Comment : TenantedBase
{
    public long CommentId { get; init; }

    [ListText]
    public required string Body { get; set; }

    public long PostId { get; set; }

    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
}