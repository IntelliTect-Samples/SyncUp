namespace IntelliTect.SyncUp.Data.Models;
public class Event : TenantedBase
{
    public long EventId { get; init; }

    [Required]
    [MaxLength(500)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    [DateType]
    public DateTimeOffset? Time { get; set; }
    public string? Location { get; set; }

    [Required]
    public long GroupId { get; init; }
    [ForeignKey(nameof(GroupId))]
    public Group Group { get; set; } = null!;

    [Coalesce]
    public class EventBehaviors(CrudContext<AppDbContext> Context) : StandardBehaviors<Event, AppDbContext>(Context)
    {
        public override ItemResult BeforeSave(SaveKind kind, Event? oldItem, Event item)
        {
            if (User.Can(Permission.Admin) || User.GetUserId() == Db.Groups.Find(item.GroupId)?.CreatedById)
            {
                return true;
            }
            return "You must be an admin or have created the group associated to this event to update it.";
        }
        public override ItemResult BeforeDelete(Event item)
        {
            if (User.Can(Permission.Admin) || User.GetUserId() == Db.Groups.Find(item.GroupId)?.CreatedById)
            {
                return true;
            }
            return "You must be an admin or have created the group associated to this event to delete it.";
        }
    }
}
