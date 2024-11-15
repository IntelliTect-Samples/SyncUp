namespace IntelliTect.SyncUp.Data.Models;

public abstract class TenantedBase
    : TrackingBase, ITenanted
{
    [InternalUse, Required, MaxLength(36)]
    public string TenantId { get; set; } = null!;
    [InternalUse]
    public Tenant? Tenant { get; set; }
}
