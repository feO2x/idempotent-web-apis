using Microsoft.AspNetCore.Builder;

namespace WebApi.CompositionRoot;

public static class EndpointPipeline
{
    public static WebApplication ConfigureEndpointPipeline(this WebApplication app)
    {
        app.UseRouting();
        app.MapHealthChecks("/health/ready");
        app.MapHealthChecks("/health/live");
        app.MapGet("/", () => "Hello World!");
        return app;
    }
}
