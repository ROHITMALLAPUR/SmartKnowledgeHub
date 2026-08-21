using System.ComponentModel.DataAnnotations;

namespace SmartKnowledgeHub.API.DTOs
{
    public class DocumentCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;


        public string FileName { get; set; } = string.Empty;

        public string? Content { get; set; }

        public string UploadedBy { get; set; } = string.Empty;
    }
}
