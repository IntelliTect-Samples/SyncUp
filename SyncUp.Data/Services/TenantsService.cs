using IntelliTect.SyncUp.Data;
using IntelliTect.SyncUp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace SyncUp.Data.Services;

[Coalesce, Service]
public class TenantsService(AppDbContext db, SignInManager<User> signInManager)
{
    [Coalesce]
    public IQueryable<Tenant> LoadTenants()
    {
        return db.Tenants.Where(x => x.IsPublic);
    }

    [Coalesce]
    public async Task<ItemResult> SwitchTenant(ClaimsPrincipal claim, string tenantId)
    {
        if (!db.Tenants.Any(t => t.TenantId == tenantId))
        {
            return "Invalid Tenant Id.";
        }

        db.ForceSetTenant(tenantId);

        var user = await db.Users.FindAsync(claim.GetUserId());
        await signInManager.RefreshSignInAsync(user!);

        return true;
    }
}
