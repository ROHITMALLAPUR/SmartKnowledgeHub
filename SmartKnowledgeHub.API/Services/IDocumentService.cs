using SmartKnowledgeHub.API.DTOs;


namespace SmartKnowledgeHub.API.Services

{
    public interface IDocumentService
    {
        Task<List<DocumentResponseDto>> GetDocumentsAsync(string userId);

        Task<DocumentResponseDto?> GetDocumentByIdAsync(int id, string userId);

        Task<DocumentResponseDto> CreateDocumentAsync(DocumentCreateDto dto, string userId);

        Task<DocumentResponseDto?> UpdateDocumentAsync(
            int id,
            DocumentUpdateDto dto);

        Task<bool> DeleteDocumentAsync(int id);

    }
}
