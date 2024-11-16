using IntelliTect.SyncUp.Data;
using IntelliTect.SyncUp.Data.Auth;
using IntelliTect.SyncUp.Web;
using IntelliTect.Coalesce;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging.Console;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using IntelliTect.SyncUp.Data.Communication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Azure.Core;
using Azure.Identity;
using IntelliTect.SyncUp.Utilities;
using Microsoft.AspNetCore.Identity;
using IntelliTect.SyncUp.Data.Models;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // Explicit declaration prevents ASP.NET Core from erroring if wwwroot doesn't exist at startup:
    WebRootPath = "wwwroot"
});

builder.Logging
    .AddConsole()
    // Filter out Request Starting/Request Finished noise:
    .AddFilter<ConsoleLoggerProvider>("Microsoft.AspNetCore.Hosting.Diagnostics", LogLevel.Warning);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.localhost.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

#region Configure Services

var services = builder.Services;

services.AddApplicationInsightsTelemetry(b =>
{
    b.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});
services.AddSingleton<ITelemetryInitializer, AppInsightsTelemetryEnricher>();
services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
{
    module.EnableSqlCommandTextInstrumentation = true;
});
// App insights filters all logs to Warning by default. We want to include our own logging.
builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("IntelliTect.SyncUp", LogLevel.Information);

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString)) throw new Exception("No connection string set");
// Make sure this runs on an ARM64 system that doesn't support localDB by using a named pipe instead
connectionString = Arm64LocalDb.UpdateArm64LocalDbConnectionString(connectionString);


services.AddDbContext<AppDbContext>(options => options
    .UseSqlServer(connectionString, opt => opt
        .EnableRetryOnFailure()
        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
    )
    // Ignored because it interferes with the construction of Coalesce IncludeTrees via .Include()
    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored))
);

services.AddCoalesce<AppDbContext>();

services.AddDataProtection()
    .PersistKeysToDbContext<AppDbContext>();

services
    .AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.ConfigureAuthentication();

services.Configure<SendGridEmailOptions>(builder.Configuration.GetSection("Communication:SendGrid"));
services.AddTransient<IEmailService, SendGridEmailService>();

services.AddSwaggerGen(c =>
{
    c.AddCoalesce();
    c.SwaggerDoc("current", new OpenApiInfo { Title = "Current API", Version = "current" });
});


services.AddScoped<SecurityService>();

// Register IUrlHelper to allow for invite link generation.
services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
services.AddScoped<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext!);
});

services.AddScoped<InvitationService>();


#endregion

#region Configure HTTP Pipeline

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseViteDevelopmentServer(c =>
    {
        c.DevServerPort = 60005;
    });

    app.MapCoalesceSecurityOverview("coalesce-security");

}

app.UseAuthentication();
app.UseAuthorization();

var containsFileHashRegex = new Regex(@"[.-][0-9a-zA-Z-_]{8}\.[^\.]*$", RegexOptions.Compiled);
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // vite puts 8-char hashes before the file extension.
        // Use this to determine if we can send a long-term cache duration.
        if (containsFileHashRegex.IsMatch(ctx.File.Name))
        {
            ctx.Context.Response.GetTypedHeaders().CacheControl = new() { Public = true, MaxAge = TimeSpan.FromDays(30) };
        }
    }
});

// For all requests that aren't to static files, disallow caching by default.
// Individual endpoints may override this.
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl = new() { NoCache = true, NoStore = true };
    await next();
});

app.MapSwagger();
app.MapScalarApiReference(c => c.OpenApiRoutePattern = "/swagger/{documentName}/swagger.json");

app.MapRazorPages();
app.MapDefaultControllerRoute();

// API fallback to prevent serving SPA fallback to 404 hits on API endpoints.
app.Map("/api/{**any}", () => Results.NotFound());

app.MapFallbackToController("Index", "Home");

#endregion

#region Launch

// Initialize/migrate database.
using (var scope = app.Services.CreateScope())
{
    var serviceScope = scope.ServiceProvider;

    // Run database migrations.
    using var db = serviceScope.GetRequiredService<AppDbContext>();
    db.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
	db.Database.Migrate();
    ActivatorUtilities.GetServiceOrCreateInstance<DatabaseSeeder>(serviceScope).Seed();

    var userManager = serviceScope.GetRequiredService<UserManager<User>>();
    await db.SeedAsync(userManager);
}

app.Run();
#endregion
