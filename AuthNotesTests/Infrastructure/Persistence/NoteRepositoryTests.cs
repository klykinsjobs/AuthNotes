using AuthNotes.Domain.Entities;
using AuthNotes.Infrastructure.Persistence;
using AuthNotesTests.Integration.TestHelpers;

namespace AuthNotesTests.Infrastructure.Persistence
{
    public class NoteRepositoryTests
    {
        [Fact]
        public async Task GetByIdAsync_ShouldThrow_WhenIdIsInvalid()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            Task act() => repo.GetByIdAsync(0);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNote_WhenNoteExists()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            var note = new Note { Title = "title", Content = "content" };
            db.Notes.Add(note);
            await db.SaveChangesAsync();

            var result = await repo.GetByIdAsync(note.Id);

            Assert.NotNull(result);
            Assert.Equal("title", result!.Title);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllNotes()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            db.Notes.AddRange(
                new Note { Title = "A" },
                new Note { Title = "B" }
            );
            await db.SaveChangesAsync();

            var result = await repo.GetAllAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddAsync_ShouldThrow_WhenNoteIsNull()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            Task act() => repo.AddAsync(null!);

            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNote()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            var note = new Note { Title = "title" };

            await repo.AddAsync(note);

            Assert.Contains(db.Notes, n => n.Title == "title");
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenNoteIsNull()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            Task act() => repo.UpdateAsync(null!);

            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateNote()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            var note = new Note { Title = "title" };
            db.Notes.Add(note);
            await db.SaveChangesAsync();

            note.Title = "title2";

            await repo.UpdateAsync(note);

            Assert.Equal("title2", db.Notes.First().Title);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenIdIsInvalid()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            Task act() => repo.DeleteAsync(0);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveNote_WhenNoteExists()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            var note = new Note { Title = "title" };
            db.Notes.Add(note);
            await db.SaveChangesAsync();

            await repo.DeleteAsync(note.Id);

            Assert.Empty(db.Notes);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDoNothing_WhenNoteNotFound()
        {
            using var db = DbFactory.CreateDb();
            var repo = new NoteRepository(db);

            await repo.DeleteAsync(999);

            Assert.Empty(db.Notes);
        }
    }
}
