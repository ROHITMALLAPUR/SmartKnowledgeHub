using System.ComponentModel.DataAnnotations;

namespace SmartKnowledgeHub.API.DTOs
{
    public class DocumentUpdateDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }

    }
}
