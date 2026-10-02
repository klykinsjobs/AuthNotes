using AuthNotes.Application.DTOs;
using AuthNotes.Application.Interfaces;
using AuthNotes.Domain.Entities;
using AuthNotes.Domain.Enums;
using AuthNotes.Infrastructure.Security;
using System.Diagnostics;

namespace AuthNotes.Application.Services
{
    public class UserService(IUserRepository repo)
    {
        private readonly IUserRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public async Task<List<UserDto>> GetAllAsync()
        {
            try
            {
                var users = await _repo.GetAllAsync();
                return [.. users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Role = u.Role
                })];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserService.GetAllAsync failed: {ex}");
                throw;
            }
        }

        public async Task AddAsync(string username, string password, Role role)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.", nameof(username));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.", nameof(password));

            if (!Enum.IsDefined(role))
                throw new ArgumentException($"Role '{role}' is not valid.", nameof(role));

            username = username.Trim();

            try
            {
                if (await _repo.UsernameExistsAsync(username))
                    throw new InvalidOperationException($"Username '{username}' is already taken.");

                var user = new User
                {
                    Username = username,
                    PasswordHash = PasswordHasher.Hash(password),
                    Role = role
                };

                await _repo.AddAsync(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserService.AddAsync failed: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(int id, string username, string? password, Role role)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "User id must be positive.");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.", nameof(username));

            if (!Enum.IsDefined(role))
                throw new ArgumentException($"Role '{role}' is not valid.", nameof(role));

            username = username.Trim();

            try
            {
                var user = await _repo.GetByIdAsync(id)
                    ?? throw new KeyNotFoundException($"User {id} not found.");

                if (!string.Equals(user.Username, username, StringComparison.OrdinalIgnoreCase)
                    && await _repo.UsernameExistsAsync(username))
                {
                    throw new InvalidOperationException($"Username '{username}' is already taken.");
                }

                user.Username = username;

                if (!string.IsNullOrWhiteSpace(password))
                    user.PasswordHash = PasswordHasher.Hash(password);

                user.Role = role;

                await _repo.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserService.UpdateAsync failed: {ex}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "User id must be positive.");

            try
            {
                await _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserService.DeleteAsync failed: {ex}");
                throw;
            }
        }
    }
}
