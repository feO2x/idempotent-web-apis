using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Serilog;
using WebApi.CompositionRoot;

namespace WebApi;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        try
        {
            var app = WebApplication
               .CreateBuilder(args)
               .ConfigureServices(Log.Logger)
               .Build()
               .ConfigureEndpointPipeline();

            await app.RunAsync();
            return 0;
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Could not run Web API");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}
