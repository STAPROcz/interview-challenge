using DocumentProcessor.Core.Entities;

namespace DocumentProcessor.Core.Interfaces;

/// <summary>
/// Repository for Document entity operations.
/// </summary>
public interface IDocumentRepository
{
    Task<Document?> GetByIdAsync(Guid id);
    Task<Document> CreateAsync(Document document);
    Task UpdateAsync(Document document);
}
