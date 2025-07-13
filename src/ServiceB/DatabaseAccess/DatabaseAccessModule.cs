using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ServiceB.DatabaseAccess;

public static class DatabaseAccessModule
{
    public static void AddDatabaseAccess(this WebApplicationBuilder builder) =>
        builder.AddNpgsqlDbContext<ServiceBDbContext>(
            "service-b-db",
            configureDbContextOptions: options => options.UseSnakeCaseNamingConvention()
        );

    public static async Task ApplyDatabaseMigrationsAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ServiceBDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}