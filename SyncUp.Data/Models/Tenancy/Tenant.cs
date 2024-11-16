using System.ComponentModel;

namespace IntelliTect.SyncUp.Data.Models;

[Read(AllowAuthenticated)]
[Edit(Roles = nameof(Permission.Admin))]
[Create(DenyAll)]
[Delete(DenyAll)]
[Display(Name = "Organization")]
public class Tenant
{
    [MaxLength(36)]
    public string TenantId { get; set; } = Guid.NewGuid().ToString();

    public required string Name { get; set; }

    public bool IsPublic { get; set; }


    [DefaultDataSource]
    public class DefaultSource(CrudContext<AppDbContext> context) : AppDataSource<Tenant>(context)
    {
        public override IQueryable<Tenant> GetQuery(IDataSourceParameters parameters)
        {
            // Only allow the current tenant to be read and modified.
            return base.GetQuery(parameters)
                .Where(t => t.TenantId == User.GetTenantId());
        }
    }

    public class GlobalAdminSource(CrudContext<AppDbContext> context) : AppDataSource<Tenant>(context)
    {
        public override IQueryable<Tenant> GetQuery(IDataSourceParameters parameters)
        {
            if (!User.IsInRole(AppClaimValues.GlobalAdminRole))
            {
                return Enumerable.Empty<Tenant>().AsQueryable();
            }

            return base.GetQuery(parameters);
        }
    }

    [Coalesce, Execute(AppClaimValues.GlobalAdminRole)]
    public static async Task<ItemResult> Create(
        AppDbContext db,
        [Inject] InvitationService invitationService,
        [Display(Name = "Org Name")] string name,
        [DataType(DataType.EmailAddress)] string adminEmail
    )
    {
        Tenant tenant = new() { Name = name };
        db.Tenants.Add(tenant);
        await db.SaveChangesAsync();

        db.ForceSetTenant(tenant.TenantId);
        new DatabaseSeeder(db).SeedNewTenant(tenant);

        return await invitationService.CreateAndSendInvitation(adminEmail, db.Roles.ToArray());
    }

    [Coalesce]
    public static async Task<ItemResult<bool>> IsMemberOf(
        AppDbContext db,
        ClaimsPrincipal user,
        string tenantId
    )
    {
        var currentTenantId = user.GetTenantId();
        var tenant = db.Tenants.FirstOrDefault(t => t.TenantId == tenantId);
        if (tenant == null)
        {
            return "Organization not found.";
        }

        db.ForceSetTenant(tenant.TenantId);

        var isMember = db.TenantMemberships.Any(tm => tm.UserId == user.GetUserId());
        db.ForceSetTenant(currentTenantId);

        return new ItemResult<bool>(isMember, null);
    }

    [Coalesce]
    public static async Task<ItemResult> JoinOrganization(
        AppDbContext db,
        ClaimsPrincipal user, 
        string tenantId
    )
    {
        var tenant = db.Tenants.FirstOrDefault(t => t.TenantId == tenantId);
        if (tenant == null)
        {
            return "Organization not found.";
        } else if (!tenant.IsPublic)
        {
            return "An invitation is required to join this organization.";
        }
        
        db.ForceSetTenant(tenant.TenantId);

        db.TenantMemberships.Add(new()
        {
            UserId = user.GetUserId()
        });
        db.SaveChanges();

        return true;
    }

    [Coalesce]
    public static async Task<ItemResult> LeaveOrganization(
       AppDbContext db,
       ClaimsPrincipal user,
       string tenantId
   )
    {
        var currentTenantId = user.GetTenantId();
        var tenant = db.Tenants.FirstOrDefault(t => t.TenantId == tenantId);
        if (tenant == null)
        {
            return "Organization not found.";
        }

        db.ForceSetTenant(tenant.TenantId);

        db.RemoveRange(db.TenantMemberships.Where(tm => tm.UserId == user.GetUserId()).ToList());
        db.SaveChanges();

        db.ForceSetTenant(currentTenantId);

        return true;
    }
}
