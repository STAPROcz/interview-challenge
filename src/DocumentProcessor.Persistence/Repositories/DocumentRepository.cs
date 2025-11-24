using DocumentProcessor.Core.Entities;
using DocumentProcessor.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocumentProcessor.Persistence.Repositories;

public class DocumentRepository(DocumentProcessorDbContext context) : IDocumentRepository
{
    public async Task<Document?> GetByIdAsync(Guid id) =>
        await context.Documents.FirstOrDefaultAsync(d => d.Id == id);

    public async Task<Document> CreateAsync(Document document)
    {
        _ = context.Documents.Add(document);
        _ = await context.SaveChangesAsync();
        return document;
    }

    public async Task UpdateAsync(Document document)
    {
        _ = context.Documents.Update(document);
        _ = await context.SaveChangesAsync();
    }
}
