﻿using System.Data;

namespace IntelliTect.SyncUp.Data;

public class DatabaseSeeder(AppDbContext db)
{
    public void Seed()
    {
    }

    public void SeedNewTenant(Tenant tenant, string? userId = null)
    {
        var tenantId = tenant.TenantId;
        db.TenantId = tenantId;

        SeedRoles();

        if (userId is not null)
        {
            // Give the first user in the tenant all roles so there is an initial admin.
            db.AddRange(db.Roles.Select(r => new UserRole { Role = r, UserId = userId }));
            db.Add(new TenantMembership { UserId = userId });
        }

        db.SaveChanges();
    }

    public void SeedTenants()
    {
        if (!db.Tenants.Any())
        {
            db.Tenants.Add(new()
            {
                Name = "Demo Tenant - Gals Need Pals",
            });
            db.SaveChanges();
        }
    }

    public void SeedRoles()
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

            db.SaveChanges();
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

    public void SeedExistingUsersWithDemoTenantAccess()
    {
        foreach (var user in db.Users)
        {
            db.TenantMemberships.Add(new()
            {
                User = user
            });
        }
        db.SaveChanges();
    }

    public void SeedGroups()
    {
        if (!db.Groups.Any())
        {
            db.Groups.AddRange(new()
            {
                Name = "Spokane",
                SubTitle = "Generic group for the Spokane area",
                Posts = [
                    new()
                    {
                        Title = "What is there to do in Spokane?",
                        Body = "Go check out IntelliTect! (And join their hackathon...)"
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
                SubTitle = "Generic group for the Seattle area",
                Posts = [new()
                {
                    Title = "What is there to do in Seattle?",
                    Body = "There is too much to do here. Go to Spokane instead!"
                }]
            },
            new()
            {
                Name = "Gym Girlies",
                SubTitle = "Everything fitness and gym related!",

            },
            new()
            {
                Name = "Soccer Moms",
                SubTitle = "Made for the busy mom life"
            },
            new()
            {
                Name = "DIY Divas",
                SubTitle = "Share DIY tips and find new inspiration",
                Posts = [
                    new()
                    {
                        Title = "What should I do with this space?",
                        Body = "Help brainstorm ideas to makeover this room."
                    }]
            });

            db.SaveChanges();
        }
    }

    public void SeedEvents()
    {
        ///TODO: Mock events
    }
}
