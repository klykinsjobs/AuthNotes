using AuthNotes.Application.DTOs;
using AuthNotes.Application.Interfaces;
using AuthNotes.Application.Services;
using AuthNotes.Domain.Entities;
using AuthNotes.Domain.Enums;
using AuthNotes.Infrastructure.Security;
using Moq;

namespace AuthNotesTests.Application.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repo = new();
        private readonly UserService _service;

        public UserServiceTests()
        {
            _service = new UserService(_repo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedDtos()
        {
            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync([new User { Id = 1, Username = "alice", Role = Role.Admin }]);

            var result = await _service.GetAllAsync();

            Assert.IsType<List<UserDto>>(result);
            Assert.Single(result);
            Assert.Equal("alice", result[0].Username);
            Assert.Equal(Role.Admin, result[0].Role);
        }

        [Fact]
        public async Task AddAsync_ShouldThrow_WhenUsernameIsEmpty()
        {
            Task act() => _service.AddAsync("", "pass", Role.User);

            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task AddAsync_ShouldThrow_WhenPasswordIsEmpty()
        {
            Task act() => _service.AddAsync("user", "", Role.User);

            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task AddAsync_ShouldThrow_WhenRoleIsInvalid()
        {
            Task act() => _service.AddAsync("user", "pass", (Role)999);

            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task AddAsync_ShouldThrow_WhenUsernameAlreadyExists()
        {
            _repo.Setup(r => r.UsernameExistsAsync("user")).ReturnsAsync(true);

            Task act() => _service.AddAsync("user", "pass", Role.User);

            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }

        [Fact]
        public async Task AddAsync_ShouldPassMappedEntityToRepository_WithHashedPassword()
        {
            User? saved = null;

            _repo.Setup(r => r.AddAsync(It.IsAny<User>()))
                 .Callback<User>(u => saved = u)
                 .Returns(Task.CompletedTask);

            await _service.AddAsync("user", "pass", Role.User);

            Assert.NotNull(saved);
            Assert.Equal(PasswordHasher.Hash("pass"), saved!.PasswordHash);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenIdIsInvalid()
        {
            Task act() => _service.UpdateAsync(0, "user", "pass", Role.User);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenUserNotFound()
        {
            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User?)null);

            Task act() => _service.UpdateAsync(1, "user", "pass", Role.User);

            await Assert.ThrowsAsync<KeyNotFoundException>(act);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenNewUsernameAlreadyExists()
        {
            var existing = new User { Id = 1, Username = "old" };

            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
            _repo.Setup(r => r.UsernameExistsAsync("new")).ReturnsAsync(true);

            Task act() => _service.UpdateAsync(1, "new", null, Role.User);

            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenNewRoleIsInvalid()
        {
            var existing = new User { Id = 1, Username = "user", Role = Role.User };
            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

            Task act() => _service.UpdateAsync(1, "user", null, (Role)999);

            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUserCorrectly()
        {
            var user = new User { Id = 1, Username = "old", PasswordHash = "hash", Role = Role.User };

            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            await _service.UpdateAsync(1, "new", "pass", Role.Admin);

            Assert.Equal("new", user.Username);
            Assert.Equal(PasswordHasher.Hash("pass"), user.PasswordHash);
            Assert.Equal(Role.Admin, user.Role);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenIdIsInvalid()
        {
            Task act() => _service.DeleteAsync(0);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
        }
    }
}
