using AuthNotes.Application.Interfaces;
using AuthNotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AuthNotes.Infrastructure.Persistence
{
    public class NoteRepository(ApplicationDbContext db) : INoteRepository
    {
        private readonly ApplicationDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

        public Task<Note?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Note id must be positive.");

            try
            {
                return _db.Notes
                    .Include(n => n.LastUpdatedBy)
                    .FirstOrDefaultAsync(n => n.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NoteRepository.GetByIdAsync failed: {ex}");
                throw;
            }
        }

        public Task<List<Note>> GetAllAsync()
        {
            try
            {
                return _db.Notes
                    .Include(n => n.LastUpdatedBy)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NoteRepository.GetAllAsync failed: {ex}");
                throw;
            }
        }

        public async Task AddAsync(Note note)
        {
            ArgumentNullException.ThrowIfNull(note);

            try
            {
                _db.Notes.Add(note);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NoteRepository.AddAsync failed: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(Note note)
        {
            ArgumentNullException.ThrowIfNull(note);

            try
            {
                _db.Notes.Update(note);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NoteRepository.UpdateAsync failed: {ex}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Note id must be positive.");

            try
            {
                var note = await _db.Notes.FindAsync(id);
                if (note != null)
                {
                    _db.Notes.Remove(note);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NoteRepository.DeleteAsync failed: {ex}");
                throw;
            }
        }
    }
}
