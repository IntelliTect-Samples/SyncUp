using IntelliTect.SyncUp.Data.Coalesce;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace IntelliTect.SyncUp.Data.Models;

[Create(nameof(Permission.UserAdmin))]
[Edit(nameof(Permission.UserAdmin))]
[Delete(nameof(Permission.UserAdmin))]
[Description("Roles are groups of permissions, analogous to job titles or functions.")]
public class Role
    : IdentityRole, ITenanted
{
    [InternalUse]
    [DefaultOrderBy(FieldOrder = 0)]
    [MaxLength(36)]
    public string TenantId { get; set; } = null!;
    [InternalUse]
    [ForeignKey(nameof(TenantId))]
    public Tenant? Tenant { get; set; }

    [Required, Search(SearchMethod = SearchMethods.Contains)]
    public override string? Name { get; set; }

    [InternalUse]
    public override string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    [InternalUse]
    public override string? NormalizedName { get; set; }

    public List<Permission>? Permissions { get; set; }

    public class Behaviors(RoleManager<Role> roleManager, CrudContext<AppDbContext> context) : AppBehaviors<Role>(context)
    {
        public override ItemResult BeforeSave(SaveKind kind, Role? oldItem, Role item)
        {
            if (AppClaimValues.GlobalAdminRole.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
            {
                return $"{item.Name} is a reserved role name and cannot be used.";
            }

            item.NormalizedName = roleManager.NormalizeKey(item.Name);

            return base.BeforeSave(kind, oldItem, item);
        }
    }
}

[InternalUse]
public class RoleClaim : IdentityRoleClaim<string>, ITenanted
{
    [ForeignKey(nameof(RoleId))]
    public Role? Role { get; set; }

    [InternalUse]
    [DefaultOrderBy(FieldOrder = 0)]
    [MaxLength(36)]
    public required string TenantId { get; set; }
    [InternalUse]
    [ForeignKey(nameof(TenantId))]
    public Tenant? Tenant { get; set; }
}