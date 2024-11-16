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

    public class EventsByDate(CrudContext<AppDbContext> context) : StandardDataSource<Event, AppDbContext>(context)
    {
        [Coalesce]
        public bool ShowPastEvents { get; set; }

        [Coalesce]
        public string? UserId { get; set; }

        public override IQueryable<Event> GetQuery(IDataSourceParameters parameters)
        {
            var query = base.GetQuery(parameters);

            if (!ShowPastEvents)
            {
                query = query.Where(e => e.Time > DateTimeOffset.Now);
            }

            if (UserId != null)
            {
                var groupIds = Db.GroupUsers.Where(gu => gu.UserId == UserId).Select(gu => gu.GroupId).Distinct();
                query = query.Where(e => groupIds.Contains(e.GroupId));
            }

            return query.OrderBy(e => e.Time);
        }

        public override IQueryable<Event> ApplyListDefaultSorting(IQueryable<Event> query)
        {
            return base.ApplyListDefaultSorting(query).OrderBy(x => x.Time);
        }
    }
}
