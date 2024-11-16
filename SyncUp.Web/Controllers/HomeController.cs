using IntelliTect.SyncUp.Data.Auth;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace IntelliTect.SyncUp.Web.Controllers;

public class HomeController() : Controller
{
    /// <summary>
    /// Spa route for vue-based parts of the app
    /// </summary>
    /// <remarks>
    /// Caching is prevented on this route because the served file contains
    /// the links to compiled js/css that include hashes in the filenames.
    /// </remarks>
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public async Task<IActionResult> Index(
        [FromServices] JavaScriptSnippet appInsightsSnippet,
        [FromServices] IWebHostEnvironment hostingEnvironment
    )
    {
        if (!User.HasTenant())
        {
            return RedirectToPage("/SelectTenant", new { ReturnUrl = Request.GetEncodedPathAndQuery() });
        }

        var fileInfo = hostingEnvironment.WebRootFileProvider.GetFileInfo("index.html");
        if (!fileInfo.Exists) return NotFound($"{hostingEnvironment.WebRootPath}/index.html was not found");

        using var reader = new StreamReader(fileInfo.CreateReadStream());
        string contents = await reader.ReadToEndAsync();

        // OPTIONAL: Inject settings or other variables into index.html here.
        // These will then be available as global variables in your Vue app.
        // Declare them as globals in env.d.ts.
        string headPrepend = $"""
        <script>
            ASPNETCORE_ENVIRONMENT="{JsEncode(hostingEnvironment.EnvironmentName)}"
        </script>
        """;

        if (appInsightsSnippet.FullScript.Length > 0)
        {
            headPrepend +=
                appInsightsSnippet.FullScript
                // Remove the automatic trackPageView event that is fired on load.
                // We fire our own page tracking events in router.ts to get better data.
                + "<script>window.appInsights.queue.pop()</script>";
        }

        contents = contents.Replace("<head>", "<head>" + headPrepend);

        return Content(contents, "text/html");

        static string JsEncode(string s) => HttpUtility.JavaScriptStringEncode(s);
    }
}
