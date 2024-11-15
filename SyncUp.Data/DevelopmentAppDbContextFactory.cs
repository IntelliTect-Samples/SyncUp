using IntelliTect.SyncUp.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IntelliTect.SyncUp.Data;

public class DevelopmentAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // This is only used when adding migrations and updating the database from the cmd line.
        // It shouldn't ever be used in code where it might end up running in production.
        var connectionString = "Server=(localdb)\\\\MSSQLLocalDB;Database=IntelliTect.SyncUp;Trusted_Connection=True;TrustServerCertificate=True;";
        if (string.IsNullOrEmpty(connectionString)) throw new Exception("No connection string set");
        // Make sure this runs on an ARM64 system that doesn't support localDB by using a named pipe instead
        connectionString = Arm64LocalDb.UpdateArm64LocalDbConnectionString(connectionString);

        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseSqlServer(connectionString);
        return new AppDbContext(builder.Options);
    }
}
