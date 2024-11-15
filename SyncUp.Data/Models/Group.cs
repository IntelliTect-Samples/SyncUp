using Microsoft.IdentityModel.Tokens;

namespace IntelliTect.SyncUp.Data.Models;

public class Group : TenantedBase
{
    public long Id { get; set; }

    [MaxLength(500)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public required string SubTitle { get; set; }
}