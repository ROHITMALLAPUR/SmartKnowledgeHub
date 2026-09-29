using Microsoft.EntityFrameworkCore;
using SmartKnowledgeHub.API.Data;
using SmartKnowledgeHub.API.DTOs;
using SmartKnowledgeHub.API.Models;

namespace SmartKnowledgeHub.API.Services
{
    public class DocumentService : IDocumentService
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public DocumentService(AppDbContext context, IWebHostEnvironment environment)
        {

            _context = context;
            _environment = environment;

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


        public async Task<DocumentResponseDto?> GetDocumentByIdAsync(int id, string userId)
        {
           

            var document = await _context.Documents.FirstOrDefaultAsync(d=>d.Id == id && d.UserId == userId);

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
            if (dto.File == null || dto.File.Length == 0)
            {
                throw new ArgumentException("File is required.");
            }

            const long maxFileSize = 10 * 1024 * 1024; // 10 MB

            if (dto.File.Length > maxFileSize)
            {
                throw new ArgumentException("File size exceeds the maximum limit of 10 MB.");
            }

            var allowedExtensions = new[] { ".pdf", ".docx", ".txt" };

            var extention = Path.GetExtension(dto.File.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extention))
            {
                throw new ArgumentException("Invalid file type. Only PDF, DOCX, and TXT files are allowed.");
            }

            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "uploads");

            Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(dto.File.FileName);

            var storedFileName = $"{Guid.NewGuid()}{extension}";

            var physicalFilePath = Path.Combine(uploadsFolder, storedFileName);

            using var stream = new FileStream(physicalFilePath, FileMode.Create);

            await dto.File.CopyToAsync(stream);
            var document = new Document
            {
                FileName=dto.File.FileName,
                FilePath = Path.Combine("uploads", storedFileName),
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


        public async Task<DocumentResponseDto?> UpdateDocumentAsync(int id, DocumentUpdateDto dto, string userId)
        {


            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

            if (document == null)
            {
                return null;

            }

            document.Title = dto.Title;
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

        public async Task<bool> DeleteDocumentAsync(int id, string userId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);

            if (document == null)
            {
                return false;
            }

            var filePath = Path.Combine(_environment.ContentRootPath, document.FilePath);


            _context.Documents.Remove(document);

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await _context.SaveChangesAsync();

            return true;
        }


    }
}
