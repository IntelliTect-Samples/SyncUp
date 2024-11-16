using IntelliTect.SyncUp.Data;

namespace SyncUp.Data.Models;
public class GroupUser : TenantedBase
{
    public long GroupUserId { get; set; }

    public bool IsOwner { get; set; }

    [Required]
    public string UserId { get; set; } = null!;
    public User? User { get; set; }

    [Required]
    public long GroupId { get; set; }
    public Group? Group { get; set; }

    public class UsersForGroup(CrudContext<AppDbContext> context) : StandardDataSource<GroupUser, AppDbContext>(context)
    {
        [Coalesce]
        public long? GroupId { get; set; }

        public override IQueryable<GroupUser> GetQuery(IDataSourceParameters parameters)
        {
            return base.GetQuery(parameters).Where(x => x.GroupId == GroupId);
        }
    }
}
