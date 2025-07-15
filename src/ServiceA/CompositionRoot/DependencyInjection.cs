using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ServiceA.Contacts;
using ServiceA.HttpAccess;
using Shared.CommonDtoValidation;
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
        services.AddOpenTelemetryMetricsAndTracing(builder.Configuration, "service-a");
        services.AddCommonDtoValidation();
        services.AddHttpAccess();
        services.AddContactsModule();
        return builder;
    }
}
