using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Serilog;
using ServiceA.CompositionRoot;
using Shared.CompositionRoot;

namespace ServiceA;

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
        catch (Exception ex)
        {
            Log.Fatal(ex, "Could not start Service A");
            throw;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}