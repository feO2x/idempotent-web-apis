using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.Configuration;
using Projects;
using Serilog;
using Shared.CompositionRoot;

namespace AspireAppHost;

public static class AppHost
{
    [Experimental("ASPIREPROXYENDPOINTS001")]
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

    [Experimental("ASPIREPROXYENDPOINTS001")]
    private static IDistributedApplicationBuilder ConfigureServices(this IDistributedApplicationBuilder builder)
    {
        builder.Services.AddSerilog();

        var postgresUserName = builder.AddParameter("postgres-user", "postgres");
        var postgresPassword = builder.AddParameter("postgres-password", "password", secret: true);

        var postgresServer = builder.AddPostgres("Postgres")
           .WithImage("postgres:17.5")
           .WithHostPort(7223)
           .WithEndpointProxySupport(proxyEnabled: false)
           .WithDataVolume()
           .WithUserName(postgresUserName)
           .WithPassword(postgresPassword)
           .WithLifetime(ContainerLifetime.Persistent);
        var serviceBDatabase = postgresServer.AddDatabase("service-b-db");

        if (builder.Configuration.GetValue<bool>("onlyStartPostgres"))
        {
            return builder;
        }

        var serviceB = builder
           .AddProject<ServiceB>("ServiceB")
           .WithReference(serviceBDatabase)
           .WaitFor(serviceBDatabase);

        builder
           .AddProject<ServiceA>("ServiceA")
           .WithReference(serviceB)
           .WaitFor(serviceB);

        return builder;
    }
}