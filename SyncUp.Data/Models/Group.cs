using SyncUp.Data.Models;

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

    [Coalesce]
    public ItemResult<bool> CheckMembership(AppDbContext db, ClaimsPrincipal claim)
    {
        var result =  db.GroupUsers.Any(x => x.GroupId == GroupId && x.UserId == claim.GetUserId());
        return new ItemResult<bool>() { Object = result };
    }

    [Coalesce]
    public async Task<ItemResult> ToggleMembership(AppDbContext db, ClaimsPrincipal claim)
    {
        // Lookup membership
        GroupUser? groupUser = await db.GroupUsers
            .FirstOrDefaultAsync(x => x.GroupId == GroupId && x.UserId == claim.GetUserId());

        // Add or remove membership
        if(groupUser is null)
        {
            db.GroupUsers.Add(new()
            {
                UserId = claim.GetUserId()!, // A user should always have an ID if logged in
                GroupId = GroupId
            });
        }
        else
        {
            db.GroupUsers.Remove(groupUser);
        }

        await db.SaveChangesAsync();

        return true;
    }
}