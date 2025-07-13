using System;
using System.Threading.Tasks;
using Aspire.Hosting;
using Projects;
using Serilog;
using Shared.CompositionRoot;

namespace AspireAppHost;

public static class AppHost
{
    public static async Task Main(string[] args)
    {
        Log.Logger = Logging.CreateLoggerForAspireHost();
        try
        {
            await using var app = DistributedApplication
               .CreateBuilder(args)
               .ConfigureServices()
               .Build();

            await app.RunAsync();
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Could not start Aspire App Host");
            throw;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static IDistributedApplicationBuilder ConfigureServices(this IDistributedApplicationBuilder builder)
    {
        builder.Services.AddSerilog();

        var serviceB = builder.AddProject<ServiceB>("ServiceB");

        builder
           .AddProject<ServiceA>("ServiceA")
           .WithReference(serviceB)
           .WaitFor(serviceB);

        return builder;
    }
}