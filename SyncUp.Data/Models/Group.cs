using SyncUp.Data.Models;

namespace IntelliTect.SyncUp.Data.Models;

public class Group : TenantedBase
{
    public long GroupId { get; init; }

    [MaxLength(500)]
    [ListText]
    [Required]
    public required string Name { get; set; }

    [ForeignKey(nameof(BannerImageId))]
    public string? BannerImageId { get; set; }
    public Image? BannerImage { get; set; }

    [Required]
    public required string Description { get; set; }

    public ICollection<Post> Posts { get; set; } = [];

    public ICollection<Event> Events { get; set; } = [];
}