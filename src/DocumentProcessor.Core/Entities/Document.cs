namespace DocumentProcessor.Core.Entities;

/// <summary>
/// Represents a document that goes through the processing pipeline.
/// </summary>
public class Document
{
    public Guid Id { get; set; }

    public DocumentState State { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UploadedAt { get; set; }

    public DateTime? ProcessingStartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? FailedAt { get; set; }

    public string? ErrorMessage { get; set; }

    /// <summary>
    /// All files associated with this document
    /// </summary>
    public List<DocumentFile> Files { get; set; } = [];

    /// <summary>
    /// Records that a file has been uploaded.
    /// Transitions state: Created -> Uploading -> UploadComplete
    /// </summary>
    public void FileUploaded(string fileName, DateTime uploadedAt)
    {
        if (State == DocumentState.Created)
        {
            State = DocumentState.Uploading;
        }

        DocumentFile? file = Files.FirstOrDefault(f =>
            f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase)
        );

        if (file != null)
        {
            file.UploadedAt = uploadedAt;
        }

        // If all files are uploaded, mark as complete
        if (Files.All(f => f.UploadedAt.HasValue))
        {
            State = DocumentState.UploadComplete;
            UploadedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Marks the document as processing.
    /// </summary>
    public void StartProcessing()
    {
        State = DocumentState.Processing;
        ProcessingStartedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the document as successfully completed.
    /// </summary>
    public void MarkCompleted()
    {
        State = DocumentState.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the document as failed with an error message.
    /// </summary>
    public void MarkFailed(string errorMessage)
    {
        State = DocumentState.Failed;
        FailedAt = DateTime.UtcNow;
        ErrorMessage = errorMessage;
    }
}
