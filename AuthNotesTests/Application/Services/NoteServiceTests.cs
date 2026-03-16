using AuthNotes.Application.DTOs;
using AuthNotes.Application.Interfaces;
using AuthNotes.Application.Services;
using AuthNotes.Domain.Entities;
using Moq;

namespace AuthNotesTests.Application.Services
{
    public class NoteServiceTests
    {
        private readonly Mock<INoteRepository> _repo = new();
        private readonly NoteService _service;

        public NoteServiceTests()
        {
            _service = new NoteService(_repo.Object);
        }


        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedDtos()
        {
            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync([new Note { Id = 1, Title = "title", Content = "content", LastUpdatedBy = new User { Username = "alice" } }]);

            var result = await _service.GetAllAsync();

            Assert.IsType<List<NoteDto>>(result);
            Assert.Single(result);
            Assert.Equal("title", result[0].Title);
            Assert.Equal("alice", result[0].LastUpdatedBy);
        }

        [Fact]
        public async Task AddAsync_ShouldThrow_WhenTitleIsEmpty()
        {
            Task act() => _service.AddAsync("", "content", 1);

            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task AddAsync_ShouldThrow_WhenUserIdIsInvalid()
        {
            Task act() => _service.AddAsync("title", "content", 0);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public async Task AddAsync_ShouldPassMappedEntityToRepository()
        {
            Note? saved = null;

            _repo.Setup(r => r.AddAsync(It.IsAny<Note>()))
                 .Callback<Note>(n => saved = n)
                 .Returns(Task.CompletedTask);

            await _service.AddAsync("title", "content", 5);

            Assert.NotNull(saved);
            Assert.Equal("title", saved!.Title);
            Assert.Equal(5, saved.LastUpdatedByUserId);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenIdIsInvalid()
        {
            Task act() => _service.UpdateAsync(0, "title", "content", 1);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenNoteNotFound()
        {
            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Note?)null);

            Task act() => _service.UpdateAsync(1, "title", "content", 1);

            await Assert.ThrowsAsync<KeyNotFoundException>(act);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateNoteCorrectly()
        {
            var note = new Note { Id = 1, Title = "title", Content = "content" };

            _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(note);

            await _service.UpdateAsync(1, "title2", "content2", 10);

            Assert.Equal("title2", note.Title);
            Assert.Equal("content2", note.Content);
            Assert.Equal(10, note.LastUpdatedByUserId);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenIdIsInvalid()
        {
            Task act() => _service.DeleteAsync(0);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
        }
    }
}
