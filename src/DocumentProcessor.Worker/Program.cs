using DocumentProcessor.Persistence;
using Wolverine;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Add Persistence layer
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("DefaultConnection")!);

// Add Wolverine for messaging
builder.UseWolverine(static opts =>
{
    // TODO: Configure RabbitMQ

    // Discover message handlers in this assembly
    _ = opts.Discovery.IncludeAssembly(typeof(Program).Assembly);
});

IHost host = builder.Build();
host.Run();
