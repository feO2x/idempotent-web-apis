using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ServiceB.Contacts;
using ServiceB.DatabaseAccess;
using Shared.CommonDtoValidation;
using Shared.CompositionRoot;

namespace ServiceB.CompositionRoot;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder, ILogger logger)
    {
        builder.AddDatabaseAccess();
        var services = builder.Services;
        services.AddHealthChecks();
        services.AddSerilog(logger);
        services.AddOpenApi();
        services.AddOpenTelemetryMetricsAndTracing(builder.Configuration, "service-b", true);
        services.AddCommonDtoValidation();
        services.AddContactsModule();
        return builder;
    }
}