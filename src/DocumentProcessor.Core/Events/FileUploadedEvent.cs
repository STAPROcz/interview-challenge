namespace DocumentProcessor.Core.Events;

/// <summary>
/// Event published when a file is uploaded for a document.
/// </summary>
public record FileUploadedEvent
{
    public required Guid DocumentId { get; init; }
    public required string FileName { get; init; }
    public required DateTime UploadedAt { get; init; }
}
