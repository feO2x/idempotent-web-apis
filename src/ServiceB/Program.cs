using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Serilog;
using ServiceB.CompositionRoot;
using ServiceB.DatabaseAccess;
using Shared.CompositionRoot;

namespace ServiceB;

public static class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = Logging.CreateLoggerForService();
        try
        {
            await using var app = WebApplication
               .CreateBuilder(args)
               .ConfigureServices(Log.Logger)
               .Build()
               .ConfigureHttpPipeline();

            await app.ApplyDatabaseMigrationsAsync();
            await app.RunAsync();
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Could not start Service B");
            throw;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}