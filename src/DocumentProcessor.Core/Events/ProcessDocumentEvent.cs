namespace DocumentProcessor.Core.Events;

/// <summary>
/// Event published when a document is ready for processing.
/// </summary>
public record ProcessDocumentEvent
{
    public required Guid DocumentId { get; init; }
}
