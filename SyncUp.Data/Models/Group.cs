namespace IntelliTect.SyncUp.Data.Models;

public class Group : TenantedBase
{
    public long GroupId { get; init; }

    [MaxLength(500)]
    [ListText]
    [Required]
    public required string Name { get; set; }

    // TODO: Allow uploading images
    [Required]
    public required string ImageUrl { get; set; }

    [Required]
    public required string Description { get; set; }

    public ICollection<Post> Posts { get; set; } = [];

    public ICollection<Event> Events { get; set; } = [];
}