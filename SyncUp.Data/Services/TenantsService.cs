using IntelliTect.SyncUp.Data;

namespace SyncUp.Data.Services;

[Coalesce, Service]
public class TenantsService(AppDbContext db)
{
    [Coalesce]
    public IQueryable<Tenant> LoadTenants()
    {
        return db.Tenants.Where(x => x.IsPublic);
    }

    [Coalesce]
    public async Task<ItemResult<IQueryable<Tenant>>> JoinOrSwitchTenant(ClaimsPrincipal claim, string tenantId)
    {
        // Check that the tenantId is valid
        var tenant = db.Tenants.FirstOrDefault(x => x.TenantId == tenantId);

        if(tenant is null)
        {
            return "Tenant Id is not valid.";
        }

        // Check if the user is already a member
        var membership = db.TenantMemberships.IgnoreTenancy()
            .Where(x => x.UserId == claim.GetUserId() && x.TenantId == tenant.TenantId)
            .FirstOrDefault();

        if (membership is not null)
        {
            await Tenant.ToggleMembership(db, claim, tenant.TenantId);
        }
        else
        {
            db.ForceSetTenant(tenant.TenantId);
        }

        return true;
    }
}
