using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartKnowledgeHub.API.Data;

namespace SmartKnowledgeHub.API.Controllers

{
    [ApiController]
    [Route("api/[Controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentController(AppDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {

            var documents = await _context.Documents.ToListAsync();

            return Ok(documents);
        }

    }

}
