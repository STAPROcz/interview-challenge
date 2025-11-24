using DocumentProcessor.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add Persistence layer
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("DefaultConnection")!);

WebApplication app = builder.Build();

// Apply database migrations
await app.Services.ApplyMigrationsAsync();

app.Run();
