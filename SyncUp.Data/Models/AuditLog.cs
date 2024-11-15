using IntelliTect.Coalesce.AuditLogging;

namespace IntelliTect.SyncUp.Data.Models;

[Edit(DenyAll)]
[Delete(DenyAll)]
[Create(DenyAll)]
[Read(nameof(Permission.ViewAuditLogs))]
public class AuditLog : DefaultAuditLog
{
    public string? UserId { get; set; }

    [Display(Name = "Changed By")]
    public User? User { get; set; }

    // NOTE: Audit logs are *optionally* tenanted because they can log changes
    // to non-tenanted entities as well. Read security is implemented in the below datasource.

    [InternalUse, MaxLength(36)]
    public string? TenantId { get; set; }
    [InternalUse]
    public Tenant? Tenant { get; set; }

    [DefaultDataSource]
    public class TenantedDataSource : AppDataSource<AuditLog>
    {
        public TenantedDataSource(CrudContext<AppDbContext> context) : base(context) { }

        public override IQueryable<AuditLog> GetQuery(IDataSourceParameters parameters)
        {
            return base.GetQuery(parameters)
                .AsNoTracking()
                .Where(al =>
                    // All ViewAuditLogs users can see logs in the current tenant
                    al.TenantId == User.GetTenantId() ||
                    // Global admins can see logs that happened outside a tenant.
                    (User.IsInRole(AppClaimValues.GlobalAdminRole) && al.TenantId == null)
                );
        }
    }
}