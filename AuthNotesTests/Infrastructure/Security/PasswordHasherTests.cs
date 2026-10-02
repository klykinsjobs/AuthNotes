using AuthNotes.Infrastructure.Security;

namespace AuthNotesTests.Infrastructure.Security
{
    public class PasswordHasherTests
    {
        [Fact]
        public void Hash_ShouldThrow_WhenPasswordIsNull()
        {
            Assert.Throws<ArgumentNullException>((Action)(() => PasswordHasher.Hash(null!)));
        }

        [Fact]
        public void Hash_ShouldReturnConsistentValue()
        {
            var hash1 = PasswordHasher.Hash("secret");
            var hash2 = PasswordHasher.Hash("secret");

            Assert.Equal(hash2, hash1);
        }

        [Fact]
        public void Verify_ShouldReturnFalse_WhenPasswordIsNull()
        {
            var result = PasswordHasher.Verify(null!, "hash");

            Assert.False(result);
        }

        [Fact]
        public void Verify_ShouldThrow_WhenHashIsNull()
        {
            Assert.Throws<ArgumentNullException>((Action)(() => PasswordHasher.Verify("pass", null!)));
        }

        [Fact]
        public void Verify_ShouldReturnTrue_WhenPasswordMatchesHash()
        {
            var hash = PasswordHasher.Hash("pass");

            Assert.True(PasswordHasher.Verify("pass", hash));
        }

        [Fact]
        public void Verify_ShouldReturnFalse_WhenPasswordDoesNotMatchHash()
        {
            var hash = PasswordHasher.Hash("correct");

            Assert.False(PasswordHasher.Verify("wrong", hash));
        }
    }
}
