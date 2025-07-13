using Serilog;
using Serilog.Events;

namespace Shared.CompositionRoot;

public static class Logging
{
    public static ILogger CreateLoggerForService() =>
        new LoggerConfiguration()
           .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .WriteTo.OpenTelemetry()
           .CreateLogger();

    public static ILogger CreateLoggerForAspireHost() =>
        new LoggerConfiguration()
           .WriteTo.Console()
           .CreateLogger();
}