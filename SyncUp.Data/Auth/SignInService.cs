using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

namespace IntelliTect.SyncUp.Data.Auth;
[Coalesce, Service]

public interface ISignInService
{
    [Execute(AllowAll)]
    Task<ItemResult> SignIn(string username, string password);
    Task<ItemResult> SignOut();
    [Execute(AllowAll)]
    Task<ItemResult> ResetPassword(string email, string password, string token);
    [Execute(AllowAll)]
    Task<ItemResult> ForgotPassword(string username);
    [Execute(AllowAll)]
    Task<ItemResult> Register(string email, string password);
    Task<ItemResult<ICollection<Tenant>>> LoadTenants(ClaimsPrincipal claims);
    Task<ItemResult> SetTenant(ClaimsPrincipal claims, string? tenantId, string? tenantName = null);
}

public class SignInService(AppDbContext Db, SignInManager<User> SignInManager, UserManager<User> UserManager, UserManagementService UserManagementService) : ISignInService
{
    public async Task<ItemResult> SignIn(string username, string password)
    {
        var result = await SignInManager.PasswordSignInAsync(username, password, true, true);
        if (result.Succeeded)
        {
            return true;
        }
        return result.IsLockedOut ? "Account locked out" : "Invalid login attempt.";
    }

    public async Task<ItemResult> SignOut()
    {
        await SignInManager.SignOutAsync();
        return true;
    }


    public async Task<ItemResult> ResetPassword(string email, string password, string token)
    {
        User? user = Db.Users.Where(u => u.Email == email).FirstOrDefault();
        if (user is null)
        {
            // Don't reveal that the user does not exist
            return true;
        }

        IdentityResult? result = await UserManager.ResetPasswordAsync(user, token, password);
        if (!result.Succeeded)
        {
            string? errors = "";
            foreach (IdentityError? error in result.Errors)
            {
                errors += $"{error.Code}{Environment.NewLine}";
            }
            return "Could not reset password " + errors;
        }

        return true;
    }

    public async Task<ItemResult> Register(string email, string password)
    {
        MailAddress.TryCreate(email, out MailAddress? validEmailResult);
        if (validEmailResult is null)
        {
            return "Email format not valid.";
        }

        var user = new User()
        {
            UserName = email,
            Email = email
        };
        //Set user as admin if there are no users
        new DatabaseSeeder(Db).InitializeFirstUser(user);

        IdentityResult? createUserResult = await UserManager.CreateAsync(user, password);

        var result = await UserManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = new System.Text.StringBuilder();
            foreach (var error in result.Errors)
            {
                errors.AppendLine(error.Description);
            }
            return errors.ToString();
        }

        var emailResult = await UserManagementService.SendEmailConfirmationRequest(user);
        if (UserManager.Options.SignIn.RequireConfirmedAccount)
        {
            return new()
            {
                WasSuccessful = true,
                Message = emailResult.Message
            };
        }
        else
        {
            await SignInManager.SignInAsync(user, isPersistent: false);
            return true;
        }

    }

    public async Task<ItemResult<ICollection<Tenant>>> LoadTenants(ClaimsPrincipal claims)
    {
        var userId = claims.GetUserId();
        var Tenants = await Db.TenantMemberships
            .IgnoreTenancy()
            .Where(tm => tm.UserId == userId)
            .Select(tm => tm.Tenant!)
            .OrderBy(t => t.Name)
            .ToListAsync();

        return Tenants;
    }

    public async Task<ItemResult> SetTenant(ClaimsPrincipal claims, string? tenantId, string? tenantName)
    {
        if (tenantId is null)
        {
            //Do Create
            if (tenantName is null)
            {
                return "Tenant Name is required";
            }
            Tenant tenant = new() { Name = tenantName };
            Db.Tenants.Add(tenant);
            await Db.SaveChangesAsync();
            tenantId = tenant.TenantId;

        }
        else
        {
            //Check valid
            if (tenantId is not null && !Db.TenantMemberships.IgnoreTenancy().Any(x => x.TenantId == tenantId))
            {
                return $"Unable to set tenant with id: {tenantId}";
            }
        }

        //Do Set
        Db.ForceSetTenant(tenantId!);
        var user = await Db.Users.FindAsync(claims.GetUserId());
        await SignInManager.RefreshSignInAsync(user!);
        return true;
    }
    public const string InvalidError = "The link is no longer valid.";
    public async Task<ItemResult> ConfirmEmail(ClaimsPrincipal claims, string code, string? newEmail)
    {
        var userId = claims.GetUserId();
        if (
            string.IsNullOrWhiteSpace(userId) ||
            string.IsNullOrWhiteSpace(code) ||
            (await UserManager.FindByIdAsync(userId)) is not { } user
        )
        {
            return InvalidError;
        }

        var result = string.IsNullOrWhiteSpace(newEmail)
            ? await UserManager.ConfirmEmailAsync(user, code)
            : await UserManager.ChangeEmailAsync(user, newEmail, code);

        if (!result.Succeeded)
        {
            return InvalidError;
        }

        if (userId == user.Id)
        {
            // The verifying user is already signed in. Refresh their session
            // so they see the new email.
            await SignInManager.RefreshSignInAsync(user);
        }
        else
        {
            // A different user was signed in. Sign the user out so they don't get confused.
            await SignInManager.SignOutAsync();
        }

        return true;
    }

    public async Task<ItemResult> ForgotPassword(string username)
    {
        User? user = await UserManager.FindByNameAsync(username);
        await UserManagementService.SendPasswordResetRequest(user);

        return true;
    }
}
