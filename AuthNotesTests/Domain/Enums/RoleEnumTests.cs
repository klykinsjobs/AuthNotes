using AuthNotes.Domain.Enums;

namespace AuthNotesTests.Domain.Enums
{
    public class RoleEnumTests
    {
        [Fact]
        public void RoleEnum_ShouldContainUserAndAdminValues()
        {
            Assert.True(Enum.IsDefined(Role.User));
            Assert.True(Enum.IsDefined(Role.Admin));

            Assert.Equal(0, (int)Role.User);
            Assert.Equal(1, (int)Role.Admin);
        }
    }
}
