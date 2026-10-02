using AuthNotes.Domain.Entities;

namespace AuthNotes.Application.Interfaces
{
    public interface INoteRepository
    {
        Task<Note?> GetByIdAsync(int id);
        Task<List<Note>> GetAllAsync();
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(int id);
    }
}
