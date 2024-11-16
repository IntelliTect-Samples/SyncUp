﻿
namespace IntelliTect.SyncUp.Data.Auth;

[Coalesce, Service]
public class SecurityService()
{
    [Coalesce, Execute(HttpMethod = HttpMethod.Get, PermissionLevel = AllowAll)]
    public UserInfo WhoAmI(ClaimsPrincipal user, AppDbContext db)
    {
        var tenant = db.Tenants.Find(user.GetTenantId());

        return new UserInfo
        {
            Id = user.GetUserId(),
            UserName = user.GetUserName(),

            Email = user.GetEmail(),
            FullName = user.FindFirstValue(AppClaimTypes.FullName),

            Roles = user.Claims
                .Where(c => c.Type == AppClaimTypes.Role)
                .Select(c => c.Value)
                .ToList(),

            Permissions = user.Claims
                .Where(c => c.Type == AppClaimTypes.Permission)
                .Select(c => c.Value)
                .ToList(),

            TenantId = user.GetTenantId(),
            TenantName = tenant?.Name,
        };
    }
}
