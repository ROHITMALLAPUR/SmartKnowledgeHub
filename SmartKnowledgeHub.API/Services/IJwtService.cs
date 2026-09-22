namespace SmartKnowledgeHub.API.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string username,string email);
    }
}
