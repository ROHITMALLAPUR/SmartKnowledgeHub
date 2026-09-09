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


        [HttpGet("{id}")]

        public async Task<IActionResult> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);

            if (document == null) 
            {
                return NotFound(id);
            }



            return Ok(document);

        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentCreateDto dto)
        {
            var document= await _documentService.CreateDocumentAsync(dto);

            return CreatedAtAction
                (nameof(GetDocumentById),
                new { id = document.Id },
                document);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentUpdateDto dto)
        {
            var document = await _documentService.UpdateDocumentAsync(id, dto);

            if(document == null)
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