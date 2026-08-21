namespace SmartKnowledgeHub.API.DTOs
{
    public class DocumentResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string? FilePath { get; set; }

        public string? Summary { get; set; }

        public string UploadedBy { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
