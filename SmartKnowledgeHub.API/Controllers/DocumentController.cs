using Microsoft.AspNetCore.Mvc;
using SmartKnowledgeHub.API.Data;

namespace SmartKnowledgeHub.API.Controllers

{
    [ApiController]
    [Route("api/[Contoller]")]
    public class DocumentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentController(AppDbContext context)
        {
            _context = context;

        }
    }

}
