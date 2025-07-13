using Microsoft.AspNetCore.Builder;
using Serilog;
using Shared.CompositionRoot;

namespace ServiceA.CompositionRoot;

public static class HttpPipeline
{
    public static WebApplication ConfigureHttpPipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.MapDefaultHealthChecks();
        return app;
    }
}