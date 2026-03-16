using AuthNotes.Domain.Entities;
using AuthNotesTests.Integration.TestHelpers;

namespace AuthNotesTests.Infrastructure.Persistence
{
    public class ApplicationDbContextTests
    {
        [Fact]
        public void OnModelCreating_ShouldConfigureUniqueUsernameIndex()
        {
            using var db = DbFactory.CreateDb();

            var index = db.Model.FindEntityType(typeof(User))!
                .GetIndexes()
                .FirstOrDefault(i => i.Properties.Any(p => p.Name == "Username"));

            Assert.NotNull(index);
            Assert.True(index!.IsUnique);
        }

        [Fact]
        public void OnModelCreating_ShouldConfigureNoteUserRelationship()
        {
            using var db = DbFactory.CreateDb();

            var fk = db.Model.FindEntityType(typeof(Note))!
                .GetForeignKeys()
                .FirstOrDefault(f => f.PrincipalEntityType.ClrType == typeof(User));

            Assert.NotNull(fk);
            Assert.Equal(nameof(Note.LastUpdatedBy), fk!.DependentToPrincipal!.Name);
        }
    }
}
