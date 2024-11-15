namespace IntelliTect.SyncUp.Data.Auth;

public class UserInfo
{
    public string? Id { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }
    public string? FullName { get; set; }

    public required ICollection<string> Roles { get; set; }
    public required ICollection<string> Permissions { get; set; }
    [MaxLength(36)]
    public string? TenantId { get; set; }
    public string? TenantName { get; set; }
}