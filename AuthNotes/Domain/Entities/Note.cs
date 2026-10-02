namespace AuthNotes.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        
        public int? LastUpdatedByUserId { get; set; }
        public User? LastUpdatedBy { get; set; }
    }
}
