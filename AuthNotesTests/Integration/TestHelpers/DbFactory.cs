using AuthNotes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthNotesTests.Integration.TestHelpers
{
    public static class DbFactory
    {
        public static ApplicationDbContext CreateDb(bool seed = false)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new ApplicationDbContext(options);

            db.Database.EnsureCreated();

            if (seed)
            {
                ApplicationDbInitializer.Seed(db);
            }

            return db;
        }
    }
}
