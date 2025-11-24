using DocumentProcessor.Core.Interfaces;
using DocumentProcessor.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentProcessor.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        string connectionString
    )
    {
        _ = services.AddDbContext<DocumentProcessorDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention()
        );

        _ = services.AddScoped<IDocumentRepository, DocumentRepository>();

        return services;
    }

    public static async Task ApplyMigrationsAsync(this IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();
        DocumentProcessorDbContext context =
            scope.ServiceProvider.GetRequiredService<DocumentProcessorDbContext>();
        await context.Database.MigrateAsync();
    }
}
