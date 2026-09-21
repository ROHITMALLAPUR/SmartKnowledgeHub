using SmartKnowledgeHub.API.DTOs;

namespace SmartKnowledgeHub.API.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
    }
}
