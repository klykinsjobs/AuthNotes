namespace AuthNotes.Application.DTOs
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string LastUpdatedBy { get; set; } = "";
    }
}
