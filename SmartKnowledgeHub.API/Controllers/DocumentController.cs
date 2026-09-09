using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using SmartKnowledgeHub.API.DTOs;
using SmartKnowledgeHub.API.Models;
using SmartKnowledgeHub.API.Services;

namespace SmartKnowledgeHub.API.Controllers

{
    [ApiController]
    [Route("api/[Controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;

        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {

            var documents = await _documentService.GetDocumentsAsync();

          

            return Ok(documents);
        }

/*
        [HttpGet("{id}")]*/

  /*      public async Task<IActionResult> GetDocumentById(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            var response = new DocumentResponseDto
            {
                Id = document.Id,
                Title = document.Title,
                FileName = document.FileName,
                Summary = document.Summary,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt
            };

            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentCreateDto dto)
        {
            var document = new Document
            {
                Title = dto.Title,
                FileName = dto.FileName,
                Content = dto.Content,
                UserId = dto.UploadedBy,
                CreatedAt = DateTime.UtcNow,
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

            return CreatedAtAction
                (nameof(GetDocumentById),
                new { id = document.Id },
                response);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentUpdateDto dto)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
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

            return Ok(response);
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);

            await _context.SaveChangesAsync();

            return NoContent();
        }*/

    }
}