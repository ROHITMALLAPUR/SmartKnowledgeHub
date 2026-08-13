namespace SmartKnowledgeHub.API.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string? FilePath { get; set; }

        public string? Content { get; set; }

        public string? Summary { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


    }
}
