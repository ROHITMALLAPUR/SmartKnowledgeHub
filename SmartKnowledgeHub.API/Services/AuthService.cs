using Microsoft.EntityFrameworkCore;
using SmartKnowledgeHub.API.Data;
using SmartKnowledgeHub.API.DTOs;
using SmartKnowledgeHub.API.Models;

namespace SmartKnowledgeHub.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context) 
        { 
          _context = context;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _context.Users.AnyAsync(u => u.Email == dto.Email);

            if (existingUser)
            {
                return false; // User with the same email already exists
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
