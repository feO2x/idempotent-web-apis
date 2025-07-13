using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Serilog;
using ServiceB.CompositionRoot;
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
               .ConfigureServices()
               .Build()
               .ConfigureHttpPipeline();

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