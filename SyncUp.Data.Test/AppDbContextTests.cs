using Microsoft.EntityFrameworkCore;

namespace IntelliTect.SyncUp.Data.Test;

public class AppDbContextTests : TestBase
{
    [Fact]
    public void ShouldNotBeMissingMigrations()
    {
        Db.Database.HasPendingModelChanges();
    }
}