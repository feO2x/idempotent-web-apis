using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using Shared.CompositionRoot;
using Shared.OpenApi;

namespace ServiceA.CompositionRoot;

public static class HttpPipeline
{
    public static WebApplication ConfigureHttpPipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.MapDefaultHealthChecks();
        app.MapPrometheusScrapingEndpoint();
        app.MapOpenApi();
        app.MapDefaultScalarApiReference("Service A");
        app.MapGet("/", () => TypedResults.LocalRedirect("/scalar/v1")).ExcludeFromDescription();
        return app;
    }
}