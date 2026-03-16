using AuthNotes.Application.Services;
using AuthNotes.Domain.Entities;
using AuthNotes.Domain.Enums;
using AuthNotes.Infrastructure.Persistence;
using AuthNotesTests.Integration.TestHelpers;

namespace AuthNotesTests.Integration
{
    public class UserServiceIntegrationTests
    {
        [Fact]
        public async Task AddUpdateDeleteUser_ShouldPersistChanges()
        {
            using var db = DbFactory.CreateDb();
            var repo = new UserRepository(db);
            var service = new UserService(repo);

            await service.AddAsync("alice", "pass", Role.User);

            Assert.Contains(db.Users, u => u.Username == "alice");

            var user = db.Users.First(u => u.Username == "alice");

            await service.UpdateAsync(user.Id, "alice2", "pass2", Role.Admin);

            var updated = db.Users.First(u => u.Id == user.Id);

            Assert.Equal("alice2", updated.Username);
            Assert.Equal(Role.Admin, updated.Role);

            await service.DeleteAsync(user.Id);

            Assert.Empty(db.Users);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            using var db = DbFactory.CreateDb();

            db.Users.Add(new User { Username = "alice", PasswordHash = "x", Role = Role.User });
            db.Users.Add(new User { Username = "bob", PasswordHash = "x", Role = Role.Admin });
            await db.SaveChangesAsync();

            var repo = new UserRepository(db);
            var service = new UserService(repo);

            var result = await service.GetAllAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task UpdateAsync_ShouldNotOverwritePassword_WhenPasswordIsNull()
        {
            using var db = DbFactory.CreateDb();
            var repo = new UserRepository(db);
            var service = new UserService(repo);

            var user = new User
            {
                Username = "alice",
                PasswordHash = "pass",
                Role = Role.User
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            await service.UpdateAsync(user.Id, "alice2", null, Role.Admin);

            var updated = db.Users.First(u => u.Id == user.Id);

            Assert.Equal("alice2", updated.Username);
            Assert.Equal(Role.Admin, updated.Role);
            Assert.Equal("pass", updated.PasswordHash);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenUserNotFound()
        {
            using var db = DbFactory.CreateDb();
            var repo = new UserRepository(db);
            var service = new UserService(repo);

            Task act() => service.UpdateAsync(999, "bob", "pass", Role.Admin);

            await Assert.ThrowsAsync<KeyNotFoundException>(act);
        }
    }
}
