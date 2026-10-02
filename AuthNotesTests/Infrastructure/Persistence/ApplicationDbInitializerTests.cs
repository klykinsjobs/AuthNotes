using AuthNotesTests.Integration.TestHelpers;

namespace AuthNotesTests.Infrastructure.Persistence
{
    public class ApplicationDbInitializerTests
    {
        [Fact]
        public void CreateDb_ShouldAddSeedUsers_WhenSeedIsTrue()
        {
            using var db = DbFactory.CreateDb(seed: true);

            Assert.Equal(2, db.Users.Count());
            Assert.Contains(db.Users, u => u.Username == "guest");
            Assert.Contains(db.Users, u => u.Username == "admin");
        }

        [Fact]
        public void CreateDb_ShouldNotAddUsers_WhenSeedIsFalse()
        {
            using var db = DbFactory.CreateDb(seed: false);

            Assert.Empty(db.Users);
        }
    }
}
