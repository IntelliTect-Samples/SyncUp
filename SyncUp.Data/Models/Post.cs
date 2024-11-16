using System.Text.RegularExpressions;

namespace IntelliTect.SyncUp.Data.Models;

public class Post : TenantedBase
{
    public long PostId { get; init; }

    [MaxLength(500)]
    [ListText]
    public required string Title { get; set; }

    public required string Body { get; set; }

    public long GroupId { get; set; }

    [ForeignKey(nameof(GroupId))]
    public Group Group { get; set; }

    public ICollection<Comment> Comments { get; set; }
}