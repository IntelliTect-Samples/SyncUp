using IntelliTect.SyncUp.Data;
using IntelliTect.SyncUp.Data.Auth;
using IntelliTect.SyncUp.Data.Models;
using IntelliTect.SyncUp.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace IntelliTect.SyncUp.Web.Pages;

[Authorize]
public class CreateTenantModel(AppDbContext db) : PageModel
{
    [Required]
    [BindProperty]
    [Display(Name = "Organization Name")]
    public string? Name { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(
        [FromServices] SignInManager<User> signInManager,
        [FromServices] ImageService imageService
    )
    {
        if (!ModelState.IsValid) return Page();

        Tenant tenant = new() { Name = Name! };
        db.Tenants.Add(tenant);
        await db.SaveChangesAsync();

        db.ForceSetTenant(tenant.TenantId);
        await (new DatabaseSeeder(db)).SeedNewTenant(tenant, imageService, User.GetUserId());

        // Sign the user into the new tenant (uses `db.TenantId`).
        var user = await db.Users.FindAsync(User.GetUserId());
        await signInManager.RefreshSignInAsync(user!);

        return Redirect("/");
    }
}
