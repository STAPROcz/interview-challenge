using DocumentProcessor.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentProcessor.Persistence;

public class DocumentProcessorDbContext(DbContextOptions<DocumentProcessorDbContext> options)
    : DbContext(options)
{
    public DbSet<Document> Documents { get; set; } = null!;
    public DbSet<DocumentFile> DocumentFiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<DocumentFile>(static entity =>
        {
            _ = entity.HasKey(static e => e.Id);
            _ = entity.Property(static e => e.Name).IsRequired();
            _ = entity.Property(static e => e.StorageKey).IsRequired();
            _ = entity.Property(static e => e.DocumentId).IsRequired();
        });

        _ = modelBuilder.Entity<Document>(static entity =>
        {
            _ = entity.HasKey(static e => e.Id);
            _ = entity.Property(static e => e.State).IsRequired();
            _ = entity.Property(static e => e.CreatedAt).IsRequired();
            _ = entity.Property(static e => e.ErrorMessage).HasMaxLength(1000);

            _ = entity
                .HasMany(static e => e.Files)
                .WithOne()
                .HasForeignKey(static e => e.DocumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            _ = entity.Navigation(static e => e.Files).AutoInclude();
        });
    }
}
