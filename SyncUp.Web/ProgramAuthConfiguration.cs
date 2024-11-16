using IntelliTect.SyncUp.Data;
using IntelliTect.SyncUp.Data.Auth;
using IntelliTect.SyncUp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace IntelliTect.SyncUp.Web;

public static class ProgramAuthConfiguration
{
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services
            .AddIdentity<User, Role>(c =>
            {
                c.ClaimsIdentity.RoleClaimType = AppClaimTypes.Role;
                c.ClaimsIdentity.EmailClaimType = AppClaimTypes.Email;
                c.ClaimsIdentity.UserIdClaimType = AppClaimTypes.UserId;
                c.ClaimsIdentity.UserNameClaimType = AppClaimTypes.UserName;

                c.User.RequireUniqueEmail = true;
                // https://pages.nist.gov/800-63-4/sp800-63b.html#passwordver
                c.Password.RequireNonAlphanumeric = false;
                c.Password.RequireDigit = false;
                c.Password.RequireUppercase = false;
                c.Password.RequireLowercase = false;
                c.Password.RequiredLength = 7;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();

        builder.Services.AddScoped<UserManagementService>();

        builder.Services
            .AddAuthentication()
            //.AddGoogle(options =>
            //{
            //    options.ClientId = config["Authentication:Google:ClientId"]!;
            //    options.ClientSecret = config["Authentication:Google:ClientSecret"]!;
            //    options.ClaimActions.MapJsonKey("pictureUrl", "picture");
            //})
            //.AddMicrosoftAccount(options =>
            //{
            //    options.ClientId = config["Authentication:Microsoft:ClientId"]!;
            //    options.ClientSecret = config["Authentication:Microsoft:ClientSecret"]!;
            //    options.SaveTokens = true;
            //})
            ;

        builder.Services.Configure<SecurityStampValidatorOptions>(o =>
        {
            // Configure how often to refresh user claims and validate
            // that the user is still allowed to sign in.
            o.ValidationInterval = TimeSpan.FromMinutes(5);
        });

        builder.Services.ConfigureApplicationCookie(c =>
        {
            c.LoginPath = "/SignIn"; // Razor page "Pages/SignIn.cshtml"

            var oldOnValidate = c.Events.OnValidatePrincipal;
            c.Events.OnValidatePrincipal = async ctx =>
            {
                // Make the current tenantID of the user available to the rest of the request.
                // This is the earliest possible point to do so after the auth cookie has been read.
                ctx.HttpContext.RequestServices
                    .GetRequiredService<AppDbContext>()
                    .TenantId = ctx.Principal?.GetTenantId();

                await oldOnValidate(ctx);
            };
        });
    }

}
