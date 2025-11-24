namespace DocumentProcessor.Core.Entities;

/// <summary>
/// Represents the current state of a document in the processing pipeline.
/// </summary>
public enum DocumentState
{
    Created,
    Uploading,
    UploadComplete,
    Processing,
    Completed,
    Failed,
}
