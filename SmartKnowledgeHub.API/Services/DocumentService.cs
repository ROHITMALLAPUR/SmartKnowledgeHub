using Microsoft.EntityFrameworkCore;
using SmartKnowledgeHub.API.Data;
using SmartKnowledgeHub.API.DTOs;
using SmartKnowledgeHub.API.Models;

namespace SmartKnowledgeHub.API.Services
{
    public class DocumentService : IDocumentService
    {

        private readonly AppDbContext _context;
        public DocumentService(AppDbContext context)
        {

            _context = context;

        }

        public async Task<List<DocumentResponseDto>> GetDocumentsAsync(string userId)
        {

            var documents = await _context.Documents.Where(d => d.UserId == userId).ToListAsync();

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


        public async Task<DocumentResponseDto?> GetDocumentByIdAsync(int id)
        {
           

            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return null;
            }


            var response = new DocumentResponseDto
            {

                Id = document.Id,
                Title = document.Title,
                FileName = document.FileName,
                FilePath = document.FilePath,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt,
                UploadedBy = document.UserId,
                Summary = document.Summary
            };

            return response;

        }

        public async Task<DocumentResponseDto> CreateDocumentAsync(DocumentCreateDto dto, string userId)
        {

            

            var document = new Document
            {
                Content = dto.Content,
                FileName = dto.FileName,
                Title = dto.Title,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Documents.Add(document);

            await _context.SaveChangesAsync();

           

            var response = new DocumentResponseDto
            {
                Id = document.Id,
                Title = document.Title,
                FileName = document.FileName,
                FilePath = document.FilePath,
                Summary = document.Summary,
                UploadedBy = document.UserId,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt
            };

            return response;
        }


        public async Task<DocumentResponseDto?> UpdateDocumentAsync(int id, DocumentUpdateDto dto)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return null;

            }

            document.Title = dto.Title;
            document.Content = dto.Content;
            document.UpdatedAt = DateTime.UtcNow;

           

            await _context.SaveChangesAsync();

           

            var response = new DocumentResponseDto
            {
                Id = document.Id,
                Title = document.Title,
                FileName = document.FileName,
                FilePath = document.FilePath,
                Summary = document.Summary,
                UploadedBy = document.UserId,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt
            };

            return response;

        }

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return false;
            }

            _context.Documents.Remove(document);

            await _context.SaveChangesAsync();

            return true;
        }


    }
}
