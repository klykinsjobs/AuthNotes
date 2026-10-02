using AuthNotes.Application.Interfaces;
using AuthNotes.Domain.Entities;
using AuthNotes.Infrastructure.Security;
using System.Diagnostics;

namespace AuthNotes.Application.Services
{
    public class AuthService(IUserRepository users)
    {
        private readonly IUserRepository _users = users ?? throw new ArgumentNullException(nameof(users));

        public async Task<User?> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.", nameof(username));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.", nameof(password));

            try
            {
                var user = await _users.GetByUsernameAsync(username.Trim());
                if (user == null)
                    return null;

                return PasswordHasher.Verify(password, user.PasswordHash) ? user : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AuthService.LoginAsync failed: {ex}");
                throw;
            }
        }
    }
}
