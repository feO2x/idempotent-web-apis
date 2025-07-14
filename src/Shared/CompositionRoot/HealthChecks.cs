using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Shared.CompositionRoot;

public static class HealthChecks
{
    public static void MapDefaultHealthChecks(this IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/health/ready");
        app.MapHealthChecks("/health/live");
    }
}
