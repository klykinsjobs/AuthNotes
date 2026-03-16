using AuthNotes.Application.Services;
using AuthNotes.Infrastructure.Persistence;
using AuthNotesTests.Integration.TestHelpers;

namespace AuthNotesTests.Integration
{
    public class AuthServiceIntegrationTests
    {
        [Fact]
        public async Task LoginAsync_ShouldAuthenticateSeededAdminUser()
        {
            using var db = DbFactory.CreateDb(seed: true);

            var repo = new UserRepository(db);
            var service = new AuthService(repo);

            var user = await service.LoginAsync("admin", "password");

            Assert.NotNull(user);
            Assert.Equal("admin", user!.Username);
        }

        [Fact]
        public async Task LoginAsync_ShouldAuthenticateSeededGuestUser()
        {
            using var db = DbFactory.CreateDb(seed: true);

            var repo = new UserRepository(db);
            var service = new AuthService(repo);

            var user = await service.LoginAsync("guest", "123");

            Assert.NotNull(user);
            Assert.Equal("guest", user!.Username);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordIsIncorrect()
        {
            using var db = DbFactory.CreateDb(seed: true);

            var repo = new UserRepository(db);
            var service = new AuthService(repo);

            var user = await service.LoginAsync("admin", "wrong");

            Assert.Null(user);
        }
    }
}
