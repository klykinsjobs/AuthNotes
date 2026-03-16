using AuthNotes.Domain.Enums;

namespace AuthNotes.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public Role Role { get; set; }
    }
}
