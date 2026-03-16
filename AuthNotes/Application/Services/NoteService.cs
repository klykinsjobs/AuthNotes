using AuthNotes.Application.DTOs;
using AuthNotes.Application.Interfaces;
using AuthNotes.Domain.Entities;
using System.Diagnostics;

namespace AuthNotes.Application.Services
{
    public class NoteService(INoteRepository repo)
    {
        private readonly INoteRepository _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public async Task<List<NoteDto>> GetAllAsync()
        {
            try
            {
                var notes = await _repo.GetAllAsync();

                return [.. notes.Select(n => new NoteDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    LastUpdatedBy = n.LastUpdatedBy?.Username ?? "Unknown"
                })];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NoteService.GetAllAsync failed: {ex}");
                throw;
            }
        }

        public async Task AddAsync(string title, string content, int userId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId), "User id must be positive.");

            try
            {
                await _repo.AddAsync(new Note
                {
                    Title = title.Trim(),
                    Content = content?.Trim() ?? "",
                    LastUpdatedByUserId = userId
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NoteService.AddAsync failed: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(int id, string title, string content, int userId)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Note id must be positive.");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId), "User id must be positive.");

            try
            {
                var note = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Note {id} not found.");

                note.Title = title.Trim();
                note.Content = content?.Trim() ?? "";
                note.LastUpdatedByUserId = userId;

                await _repo.UpdateAsync(note);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NoteService.UpdateAsync failed: {ex}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Note id must be positive.");

            try
            {
                await _repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NoteService.DeleteAsync failed: {ex}");
                throw;
            }
        }
    }
}
