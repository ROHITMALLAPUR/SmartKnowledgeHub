using SmartKnowledgeHub.API.DTOs;

namespace SmartKnowledgeHub.API.Services
{
    public interface IDocumentService
    {
        Task<List<DocumentResponseDto>> GetDocumentsAsync();

        Task<DocumentResponseDto?> GetDocumentByIdAsync(int id);

        Task<DocumentResponseDto> CreateDocumentAsync(DocumentCreateDto dto);

        Task<DocumentResponseDto?> UpdateDocumentAsync(
            int id,
            DocumentUpdateDto dto);

        Task<bool> DeleteDocumentAsync(int id);

    }
}
