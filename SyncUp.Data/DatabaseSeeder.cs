using IntelliTect.SyncUp.Data.Services;
using System.Data;
using System.Runtime.CompilerServices;

namespace IntelliTect.SyncUp.Data;

public class DatabaseSeeder(AppDbContext db)
{
    public void Seed()
    {
    }

    public async Task SeedNewTenant(Tenant tenant, ImageService? imageService, string? userId = null)
    {
        var tenantId = tenant.TenantId;
        db.TenantId = tenantId;
        tenant.IsPublic = false;

        await SeedRoles();

        if (userId is not null)
        {
            // Give the first user in the tenant all roles so there is an initial admin.
            db.AddRange(db.Roles.Select(r => new UserRole { Role = r, UserId = userId }));
            db.Add(new TenantMembership { UserId = userId });
        }

        await db.SaveChangesAsync();
    }

    public async Task SeedDemoTenant()
    {
        if (!db.Tenants.Any(t => t.Name.Contains("Demo Tenant")))
        {
            db.Tenants.Add(new()
            {
                Name = "Demo Tenant - Gals Need Pals",
                IsPublic = true,
            });
            await db.SaveChangesAsync();
        }
    }

    public async Task SeedRoles()
    {
        if (!db.Roles.Any())
        {
            db.Roles.Add(new()
            {
                Permissions = Enum.GetValues<Permission>().ToList(),
                Name = "Admin",
                NormalizedName = "ADMIN",
            });

            // NOTE: In this application's permissions-based authorization system,
            // additional roles can freely be created by administrators.
            // You don't have to seed every possible role.

            await db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Grant administrative permissions to the very first user in the application.
    /// </summary>
    public void InitializeFirstUser(User user)
    {
        if (db.Users.Any()) return;

        // If this user is the first user, make them the global admin
        user.IsGlobalAdmin = true;
    }

    public async Task SeedExistingUsersWithDemoTenantAccess()
    {
        foreach (var user in db.Users)
        {
            if (!await db.TenantMemberships.AnyAsync(tm => tm.UserId == user.Id))
            {
                db.TenantMemberships.Add(new()
                {
                    User = user
                });
            }
        }
        await db.SaveChangesAsync();
    }

    public async Task SeedGroups(ImageService? imageService)
    {
        if (!await db.Groups.AnyAsync())
        {
            db.Groups.AddRange(new()
            {
                Name = "Spokane",
                Description = "Generic group for the Spokane area",
                BannerImage = imageService == null ? null : await imageService.AddImage("https://wallpapers.com/images/hd/scenic-view-of-spokane-city-at-dusk-dqhl672fwbv622gt.jpg"),
                Posts = [
                    new()
                    {
                        Title = "What is there to do in Spokane?",
                        Body = "Go check out IntelliTect! (And join their hackathon...)",
                        Comments = [
                            new()
                            {
                                Body = "What is a hackathon?"
                            },
                            new(){
                                Body = "Check out the Looff Carousel. It's kinda cool."
                            }]
                    },
                    new()
                    {
                        Title = "Summer Activities",
                        Body = "Learn more about Spokane's festivals like Bloomsday, Pig Out in the Park, etc."
                    }]
            },
            new()
            {
                Name = "Seattle",
                Description = "Generic group for the Seattle area",
                BannerImage = imageService == null ? null : await imageService.AddImage("https://i.pinimg.com/736x/5d/d2/38/5dd238ea8ac9ee41002b8037417ee5de.jpg"),
                Posts = [
                    new()
                    {
                        Title = "What is there to do in Seattle?",
                        Body = "There is too much to do here. Go to Spokane instead!"
                    }]
            },
            new()
            {
                Name = "Gym Girlies",
                BannerImage = imageService == null ? null : await imageService.AddImage("https://www.foundationalconcepts.com/wp-content/uploads/2016/09/meditation.jpg"),
                Description = "Everything fitness and gym related!",

            },
            new()
            {
                Name = "Soccer Moms",
                BannerImage = imageService == null ? null : await imageService.AddImage("https://st3.depositphotos.com/3971595/16719/i/450/depositphotos_167194490-stock-photo-image-of-family-mother-and.jpg"),
                Description = "Made for the busy mom life"
            },
            new()
            {
                Name = "DIY Divas",
                BannerImage = imageService == null ? null : await imageService.AddImage("https://wallpapersmug.com/download/1600x900/128b37/wolf-disco-jockey-art.jpg"),
                Description = "Share DIY tips and find new inspiration",
                Posts = [
                    new()
                    {
                        Title = "What should I do with this space?",
                        Body = "Help brainstorm ideas to makeover this room.",
                        Comments = [
                            new(){
                                Body = "Make it blue!"
                            },
                            new(){
                                Body = "Make it pink!"
                            }]
                    }]
            });

            db.SaveChanges();
        }
    }

    public void SeedEvents()
    {
        var spokaneGroup = db.Groups.FirstOrDefault(g => g.Name == "Spokane");
        if (spokaneGroup != null)
        {
            db.Events.AddRange([
                new()
                {
                    Name = "Friendsgiving",
                    Description = "Get together with your friends and celebrate before going home for the holidays.",
                    Time = new DateTimeOffset(2024, 11, 25, 18, 0, 0, TimeSpan.Zero),
                    Location = "123 ABC St Spokane, WA",
                    Group = spokaneGroup
                },
                new ()
                {
                    Name = "Holiday Coffee Chat",
                    Description = "Get together with your friends and celebrate before going home for the holidays.",
                    Time = new DateTimeOffset(2024, 12, 1, 8, 30, 0, TimeSpan.Zero),
                    Location = "123 ABC St Spokane, WA",
                    Group = spokaneGroup,
                }]);
            db.SaveChanges();
        }

        var gymGroup = db.Groups.FirstOrDefault(g => g.Name == "Gym Girlies");
        if (gymGroup != null)
        {
            db.Events.AddRange([
                new()
                {
                    Name = "Riverside Run",
                    Description = "Run away from your problems and burn some energy running with us down riverside!",
                    Time = new DateTimeOffset(2024, 12, 1, 8, 30, 0, TimeSpan.Zero),
                    Location = "789 Riverside Ln Spokane, WA",
                    Group = gymGroup,
            }]);
            db.SaveChanges();
        }
    }
}
