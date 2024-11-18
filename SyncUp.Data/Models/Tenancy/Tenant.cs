using IntelliTect.SyncUp.Data.Services;
using SyncUp.Data.Models;
using System.ComponentModel;
using File = IntelliTect.Coalesce.Models.File;

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

    public string? Description { get; set; }

    public bool IsPublic { get; set; }

    public string? BannerImageId { get; set; }
    [ForeignKey(nameof(BannerImageId))]
    public Image? BannerImage { get; set; }

    [Coalesce]
    public async Task<ItemResult<Image>> UploadImageFile(AppDbContext db, [Inject] ImageService imageService, File file)
    {
        try
        {
            Image image = await imageService.AddImage(file);

            BannerImageId = image.ImageId;
            await db.SaveChangesAsync();

            return image;
        }
        catch(Exception ex)
        {
            return ex.Message;
        }
    }

    [Coalesce]
    public async Task<ItemResult<Image>> UploadImageUrl(AppDbContext db, [Inject] ImageService imageService, string url)
    {
        try
        {
            Image image = await imageService.AddImage(url);

            BannerImageId = image.ImageId;
            await db.SaveChangesAsync();

            return image;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

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
        [Inject] ImageService imageService,
        [Display(Name = "Org Name")] string name,
        [DataType(DataType.EmailAddress)] string adminEmail
    )
    {
        Tenant tenant = new() { Name = name };
        db.Tenants.Add(tenant);
        await db.SaveChangesAsync();

        db.ForceSetTenant(tenant.TenantId);
        await (new DatabaseSeeder(db)).SeedNewTenant(tenant, imageService);

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
    public static async Task<ItemResult> ToggleMembership(
        AppDbContext db,
        ClaimsPrincipal user,
        string tenantId
    )
    {
        var tenant = db.Tenants.FirstOrDefault(t => t.TenantId == tenantId);
        if (tenant == null)
        {
            return "Organization not found.";
        }

        db.ForceSetTenant(tenant.TenantId);
        var isMember = db.TenantMemberships.Any(tm => tm.UserId == user.GetUserId());

        if (!isMember)
        {
            if (!tenant.IsPublic)
            {
                return "An invitation is required to join this organization.";
            }

            db.TenantMemberships.Add(new()
            {
                UserId = user.GetUserId()
            });
        }
        else
        {
            db.RemoveRange(db.TenantMemberships.Where(tm => tm.UserId == user.GetUserId()).ToList());
        }
        db.SaveChanges();

        return true;
    }
}
