using System.ComponentModel.DataAnnotations;

namespace SmartKnowledgeHub.API.DTOs
{
    public class DocumentCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;


        [Required]
        public IFormFile File { get; set; } = null!;

    }
}
