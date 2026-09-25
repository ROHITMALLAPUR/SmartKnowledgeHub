using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using SmartKnowledgeHub.API.DTOs;
using SmartKnowledgeHub.API.Models;
using SmartKnowledgeHub.API.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace SmartKnowledgeHub.API.Controllers

{
    [Authorize]
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var documents = await _documentService.GetDocumentsAsync(userId);


            return Ok(documents);
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetDocumentById(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
            {
                return Unauthorized();
            }

            var document = await _documentService.GetDocumentByIdAsync(id, userId);

            if (document == null)
            {
                return NotFound(id);
            }



            return Ok(document);

        }

        [AllowAnonymous]

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentCreateDto dto)
        {
            //Console.WriteLine("POST METHOD EXECUTED");

            var userId = User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            //Console.WriteLine($"User ID: {userId}");

            if (userId == null)
            {
                return Unauthorized();
            }



            var document = await _documentService.CreateDocumentAsync(dto, userId);

            return CreatedAtAction
                (nameof(GetDocumentById),
                new { id = document.Id },
                document);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentUpdateDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var document = await _documentService.UpdateDocumentAsync(id, dto, userId);


            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _documentService.DeleteDocumentAsync(id);

            if (!document)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}