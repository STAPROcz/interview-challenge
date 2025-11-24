namespace DocumentProcessor.Core.Entities;

/// <summary>
/// Represents a file associated with a document.
/// </summary>
public class DocumentFile
{
    public DocumentFile() { }

    public DocumentFile(string name, string storagePrefix)
    {
        Name = name;
        StorageKey = $"{storagePrefix}/{name}";
    }

    public Guid Id { get; set; }

    /// <summary>
    /// Original filename
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Key used to access the file in storage (includes prefix/path)
    /// </summary>
    public string StorageKey { get; set; } = default!;

    /// <summary>
    /// Timestamp when the file was uploaded. Null if not yet uploaded.
    /// </summary>
    public DateTime? UploadedAt { get; set; }

    public Guid DocumentId { get; set; }
}
