using Light.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Shared.CompositionRoot;

public static class ObservabilityExtensions
{
    public static IServiceCollection AddOpenTelemetryMetricsAndTracing(
        this IServiceCollection services,
        IConfiguration configuration,
        string serviceName,
        bool useEntityFrameworkCoreInstrumentation = false
    )
    {
        var openTelemetryBuilder = services
           .AddOpenTelemetry()
           .ConfigureResource(resource => resource.AddService(serviceName))
           .WithMetrics(metrics => metrics
                           .AddAspNetCoreInstrumentation()
                           .AddHttpClientInstrumentation()
                           .AddRuntimeInstrumentation()
                           .AddPrometheusExporter()
            )
           .WithTracing(tracing =>
            {
                tracing
                   .AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation();
                if (useEntityFrameworkCoreInstrumentation)
                {
                    tracing.AddEntityFrameworkCoreInstrumentation();
                }
            });

        if (configuration.IsOtelExportEndpointPresent())
        {
            openTelemetryBuilder.UseOtlpExporter();
        }

        return services;
    }

    private static bool IsOtelExportEndpointPresent(this IConfiguration configuration) =>
        !configuration["OTEL_EXPORTER_OTLP_ENDPOINT"].IsNullOrWhiteSpace();
}