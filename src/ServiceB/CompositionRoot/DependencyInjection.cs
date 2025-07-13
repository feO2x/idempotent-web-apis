using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.CompositionRoot;

namespace ServiceB.CompositionRoot;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddHealthChecks();
        services.AddSerilog();
        services.AddOpenTelemetryMetricsAndTracing(builder.Configuration, "Service B");
        return builder;
    }
}