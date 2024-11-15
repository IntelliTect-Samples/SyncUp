using IntelliTect.SyncUp.Data;
using IntelliTect.SyncUp.Data.Models;
using IntelliTect.Coalesce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace IntelliTect.SyncUp.Web.Pages;

[AllowAnonymous]
public class ExternalLoginModel(
    AppDbContext db,
    SignInManager<User> signInManager,
    ILogger<ExternalLoginModel> logger
) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    public string? ErrorMessage { get; set; }

    public IActionResult OnGet() => RedirectToPage("Login");

    public IActionResult OnPost(string provider)
    {
        // Request a redirect to the external login provider.
        var redirectUrl = Url.Page("ExternalLogin", pageHandler: "Callback", values: new { ReturnUrl });
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return new ChallengeResult(provider, properties);
    }

    public async Task<IActionResult> OnGetCallbackAsync(string? remoteError = null)
    {
        if (remoteError != null)
        {
            ErrorMessage = $"Error from external provider: {remoteError}";
            return Page();
        }

        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null) return RedirectToPage("SignIn");

        switch (info.LoginProvider)
        {
            case GoogleDefaults.AuthenticationScheme:
                return await OnGoogleTicketReceived(info);
            case MicrosoftAccountDefaults.AuthenticationScheme:
                return await OnMicrosoftTicketReceived(info);
            default:
                ErrorMessage = "Unknown external provider";
                return Page();
        }
    }

    private async Task<IActionResult> OnGoogleTicketReceived(ExternalLoginInfo info)
    {
        var result = await GetOrCreateUser(info);
        if (!result.WasSuccessful || result.Object is not User user)
        {
            return Forbid(result.Message);
        }


        // Populate or update user photo from Google
        await UpdateUserPhoto(user,
            HttpContext.RequestServices.GetRequiredService<IOptions<GoogleOptions>>().Value.Backchannel,
            () => new HttpRequestMessage(HttpMethod.Get, info.Principal!.FindFirstValue("pictureUrl")));

        // OPTIONAL: Populate additional fields on `user` specific to Google, if any.

        await signInManager.UserManager.UpdateAsync(user);

        return await SignInExternalUser(info);
    }

    private async Task<IActionResult> OnMicrosoftTicketReceived(ExternalLoginInfo info)
    {
        var result = await GetOrCreateUser(info);
        if (!result.WasSuccessful || result.Object is not User user)
        {
            return Forbid(result.Message);
        }


        // Populate or update user photo from Microsoft Graph
        await UpdateUserPhoto(user,
            HttpContext.RequestServices.GetRequiredService<IOptions<MicrosoftAccountOptions>>().Value.Backchannel,
            () =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/photos/96x96/$value");
                request.Headers.Authorization = new("Bearer", info.AuthenticationProperties!.GetTokenValue(OpenIdConnectParameterNames.AccessToken));
                return request;
            });

        // OPTIONAL: Populate additional fields on `user` specific to Microsoft, if any.

        await signInManager.UserManager.UpdateAsync(user);

        return await SignInExternalUser(info);
    }

    private async Task<ItemResult<User>> GetOrCreateUser(ExternalLoginInfo info)
    {
        var remoteProvider = info.LoginProvider;
        var remoteUser = info.Principal!;
        var remoteUserId = remoteUser.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var remoteUserEmail = remoteUser.FindFirstValue(ClaimTypes.Email);

        // Find by the external user ID
        bool foundByLogin = false;
        User? user = await signInManager.UserManager.FindByLoginAsync(remoteProvider, remoteUserId);

        // If not found, look for an existing user by email address
        if (user is not null)
        {
            foundByLogin = true;
        }
        else if (remoteUserEmail is not null)
        {
            user = await signInManager.UserManager.FindByEmailAsync(remoteUserEmail);
            if (user?.EmailConfirmed == false)
            {
                // Don't match existing users by email if the email isn't confirmed.
                // https://security.stackexchange.com/questions/260562/adding-sso-to-an-existing-website-should-sso-login-link-to-matching-email-addr
                // It is crucial for security that you don't just mark the existing account as verified,
                // as it may have a password attached that is controlled/known by a different person
                // who is squatting on an email address in the system on hopes of hijacking the account.
                // The person who owns the current verified external login needs to sign into that account with its password,
                // including performing a "forgot password" request if the password isn't actually known.
                return $"An existing unverified user account with email address {remoteUserEmail} already exists. " +
                    $"You must log into this account with its username and password and verify the account's email address " +
                    $"before you can link the account to your {info.ProviderDisplayName} login.";
            }
        }

        if (user is null)
        {
            if (await CanUserSignUpAsync(info) is { WasSuccessful: false } canSignIn) return new(canSignIn);

            user = new User { UserName = remoteUserEmail, Email = remoteUserEmail, EmailConfirmed = true };

            new DatabaseSeeder(db).InitializeFirstUser(user);

            var result = await signInManager.UserManager.CreateAsync(user);
            if (!result.Succeeded) return string.Join(", ", result.Errors);
        }

        if (!foundByLogin)
        {
            var result = await signInManager.UserManager.AddLoginAsync(user, info);
            if (!result.Succeeded) return string.Join(", ", result.Errors);
        }

        user.FullName = remoteUser.FindFirstValue(ClaimTypes.Name) ?? user.FullName;
        if (!string.IsNullOrWhiteSpace(remoteUserEmail))
        {
            user.Email = remoteUserEmail;
            user.EmailConfirmed = true;
        }
        // OPTIONAL: Update any other properties on the user as desired.

        return user;
    }



    private async Task UpdateUserPhoto(User user, HttpClient client, Func<HttpRequestMessage> requestFactory)
    {
        UserPhoto? photo = user.Photo = db.UserPhotos.Where(p => p.UserId == user.Id).FirstOrDefault();
        if (photo is not null && photo.ModifiedOn >= DateTimeOffset.Now.AddDays(-7))
        {
            // User photo already populated and reasonably recent.
            return;
        }

        var request = requestFactory();

        if (request.RequestUri is null) return;

        try
        {
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode) return;

            byte[] content = await response.Content.ReadAsByteArrayAsync();

            if (content is not { Length: > 0 }) return;

            if (photo is null)
            {
                user.Photo = photo = new UserPhoto { UserId = user.Id, Content = content };
            }
            else
            {
                photo.Content = content;
                photo.SetTracking(user.Id);
            }
            user.PhotoHash = MD5.HashData(content);
        }
        catch { /* Failure is non-critical */ }
    }

    private Task<ItemResult> CanUserSignUpAsync(ExternalLoginInfo remoteLoginInfo)
    {
        // OPTIONAL: Examine the properties of `remoteLoginInfo` and determine if the user is permitted to sign up.
        return Task.FromResult(new ItemResult(true));
    }

    private async Task<IActionResult> SignInExternalUser(ExternalLoginInfo remoteLoginInfo)
    {
        // ExternalLoginSignInAsync checks that the user isn't locked out.
        var result = await signInManager.ExternalLoginSignInAsync(
            remoteLoginInfo.LoginProvider,
            remoteLoginInfo.ProviderKey,
            isPersistent: true,
            bypassTwoFactor: true);

        if (!result.Succeeded)
        {
            return Forbid(result.IsLockedOut ? "Account locked." : "Unable to sign in.");
        }

        // OPTIONAL: If desired, save the user's OAuth tokens for later user:
        // (you'll need to request offline_access OAuth scope for this to be of any real use).
        // await signInManager.UpdateExternalAuthenticationTokensAsync(remoteLoginInfo);

        return LocalRedirect(ReturnUrl ?? "/");
    }

    private IActionResult Forbid(string? message = null)
    {
        message ??= "Forbidden";

        ErrorMessage = message;

        return new PageResult { StatusCode = StatusCodes.Status403Forbidden };
    }
}
