using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.CompositionRoot;

namespace ServiceA.CompositionRoot;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder, ILogger logger)
    {
        var services = builder.Services;
        services.AddHealthChecks();
        services.AddSerilog(logger);
        services.AddOpenApi();
        services.AddOpenTelemetryMetricsAndTracing(builder.Configuration, "ServiceA");
        services.AddHttpClientDefaults();
        return builder;
    }
}