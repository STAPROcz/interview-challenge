# Document Processor - Interview Challenge

This is a simplified document processing system that demonstrates event-driven architecture using .NET 9, Wolverine, and PostgreSQL.

## Prerequisites

- .NET 9.0 SDK
- Docker and Docker Compose

## Getting Started

### 1. Start Infrastructure

Start PostgreSQL and RabbitMQ using Docker Compose:

```bash
docker-compose up -d
```

This will start:

- PostgreSQL on port 5432
- RabbitMQ on port 5672 (Management UI on port 15672)

### 2. Run Database Migrations

The API will automatically apply migrations on startup. Alternatively, you can manually run:

```bash
cd src/DocumentProcessor.Api
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Run the API

```bash
cd src/DocumentProcessor.Api
dotnet run
```

The API will be available at `http://localhost:5000`.

### 4. Run the Worker

In a separate terminal:

```bash
cd src/DocumentProcessor.Worker
dotnet run
```

## Architecture Overview

The system consists of three main projects:

### DocumentProcessor.Core

Contains domain entities, events, and interfaces:

- **Document** - Main entity with state machine (Created → Uploading → UploadComplete → Processing → Completed/Failed)
- **DocumentFile** - Represents files associated with a document
- **Events** - FileUploadedEvent, ProcessDocumentEvent

### DocumentProcessor.Api

RESTful API using Wolverine.Http for HTTP endpoints. Handles:

- Creating documents and generating upload credentials
- Receiving file upload notifications
- Querying document status

### DocumentProcessor.Worker

Background worker service that processes documents asynchronously.

### DocumentProcessor.Persistence

Database access layer using Entity Framework Core and PostgreSQL.

## Your Tasks

### Task 1: Implement the Create Document Endpoint

Create a POST endpoint at `/api/documents` that:

1. Accepts a request with a list of file names
2. Creates a Document entity with associated DocumentFile entities
3. Generates mock upload URLs for each file
4. Returns the document ID and upload URLs

**Expected Request:**

```json
{
  "fileNames": ["document1.pdf", "document2.pdf"]
}
```

**Expected Response:**

```json
{
  "documentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "uploadUrls": {
    "document1.pdf": "https://storage.example.com/upload/...",
    "document2.pdf": "https://storage.example.com/upload/..."
  }
}
```

**Hints:**

- For mock URLs, use: `$"https://storage.example.com/upload/{documentId}/{fileName}"`

### Task 2: Implement File Upload Handler

Create a Wolverine message handler that:

1. Listens for `FileUploadedEvent` messages
2. Updates the document's state when a file is uploaded
3. Publishes a `ProcessDocumentEvent` when all files are uploaded

### Task 3: Implement Document Processing Worker

Create a Wolverine message handler in the Worker project that:

1. Listens for `ProcessDocumentEvent` messages
2. Simulates document processing (5 second delay)
3. Updates the document state to Completed

### Task 4: Implement Get Document Status Endpoint

Create a GET endpoint at `/api/documents/{id}` that:

1. Retrieves a document by ID
2. Returns the document's current state and timestamps

**Expected Response:**

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "state": "Processing",
  "createdAt": "2024-01-15T10:30:00Z",
  "uploadedAt": "2024-01-15T10:31:00Z",
  "processingStartedAt": "2024-01-15T10:31:05Z"
}
```

## Bonus Tasks

If you complete the main tasks early, try these:

1. **Error Handling**: Implement error handling that marks documents as Failed
2. **Validation**: Add input validation for the create endpoint
3. **Unit Tests**: Write unit tests for the Document entity's state transitions
4. **Health Checks**: Add health check endpoints for the API and Worker

## Testing Your Implementation

1. Create a document:

```bash
curl -X POST http://localhost:5000/api/documents \
  -H "Content-Type: application/json" \
  -d '{"fileNames": ["test1.pdf", "test2.pdf"]}'
```

2. Simulate file uploads by publishing events to RabbitMQ or calling an endpoint you create

3. Check document status:

```bash
curl http://localhost:5000/api/documents/{id}
```

4. Observe the Worker logs to see processing activity

## RabbitMQ Management

Access the RabbitMQ Management UI at <http://localhost:15672> (guest/guest) to:

- View queues and messages
- Monitor message flow
- Debug message routing

## Tips

- Use Wolverine's conventions for message handling
- Enable detailed logging to debug message flow
- Use `IMessageBus.PublishAsync()` to send messages
- Wolverine automatically discovers message handlers in the assembly

## Resources

- [Wolverine Documentation](https://wolverine.netlify.app/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core/)
- [RabbitMQ Tutorials](https://www.rabbitmq.com/getstarted.html)

Good luck!
