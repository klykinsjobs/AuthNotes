using AuthNotes.Application.Interfaces;
using AuthNotes.Application.Services;
using AuthNotes.Domain.Entities;
using AuthNotes.Infrastructure.Security;
using Moq;

namespace AuthNotesTests.Application.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _users = new();
        private readonly AuthService _service;

        public AuthServiceTests()
        {
            _service = new AuthService(_users.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrow_WhenUsernameIsEmpty()
        {
            Task act() => _service.LoginAsync("", "pass");

            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrow_WhenPasswordIsEmpty()
        {
            Task act() => _service.LoginAsync("user", "");

            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserNotFound()
        {
            _users.Setup(r => r.GetByUsernameAsync("user")).ReturnsAsync((User?)null);

            var result = await _service.LoginAsync("user", "pass");

            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordDoesNotMatch()
        {
            var user = new User { Username = "user", PasswordHash = PasswordHasher.Hash("correct") };

            _users.Setup(r => r.GetByUsernameAsync("user")).ReturnsAsync(user);

            var result = await _service.LoginAsync("user", "wrong");

            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnUser_WhenCredentialsAreValid()
        {
            var user = new User { Username = "user", PasswordHash = PasswordHasher.Hash("pass") };

            _users.Setup(r => r.GetByUsernameAsync("user")).ReturnsAsync(user);

            var result = await _service.LoginAsync("user", "pass");

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task LoginAsync_ShouldRethrow_WhenRepositoryThrows()
        {
            _users.Setup(r => r.GetByUsernameAsync("user")).ThrowsAsync(new Exception("DB error"));

            Task act() => _service.LoginAsync("user", "pass");

            await Assert.ThrowsAsync<Exception>(act);
        }
    }
}
