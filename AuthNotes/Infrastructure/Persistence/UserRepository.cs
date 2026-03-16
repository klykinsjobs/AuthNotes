using AuthNotes.Application.Interfaces;
using AuthNotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AuthNotes.Infrastructure.Persistence
{
    public class UserRepository(ApplicationDbContext db) : IUserRepository
    {
        private readonly ApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

        public Task<User?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "User id must be positive.");

            try
            {
                return _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserRepository.GetByIdAsync failed: {ex}");
                throw;
            }
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.", nameof(username));

            username = username.Trim();

            try
            {
                return _db.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserRepository.GetByUsernameAsync failed: {ex}");
                throw;
            }
        }

        public Task<List<User>> GetAllAsync()
        {
            try
            {
                return _db.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserRepository.GetAllAsync failed: {ex}");
                throw;
            }
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.", nameof(username));

            username = username.Trim();

            try
            {
                return _db.Users.AnyAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserRepository.UsernameExistsAsync failed: {ex}");
                throw;
            }
        }

        public async Task AddAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            try
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserRepository.AddAsync failed: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            try
            {
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserRepository.UpdateAsync failed: {ex}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "User id must be positive.");

            try
            {
                var user = await _db.Users.FindAsync(id);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UserRepository.DeleteAsync failed: {ex}");
                throw;
            }
        }
    }
}
