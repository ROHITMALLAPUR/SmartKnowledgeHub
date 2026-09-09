using Microsoft.EntityFrameworkCore;
using SmartKnowledgeHub.API.Data;
using SmartKnowledgeHub.API.DTOs;

namespace SmartKnowledgeHub.API.Services
{
    public class DocumentService : IDocumentService
    {

        private readonly AppDbContext _context;
        public DocumentService(AppDbContext context)
        {

            _context = context;

        }

        public async Task<List<DocumentResponseDto>> GetDocumentsAsync()
        {

            var documents = await _context.Documents.ToListAsync();

            var response = documents.Select(document => new DocumentResponseDto
            {
                Id = document.Id,
                Title = document.Title,
                FileName = document.FileName,
                FilePath = document.FilePath,
                Summary = document.Summary,
                UploadedBy = document.UserId,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt
            }).ToList();

            return response;

        }


    }
}
