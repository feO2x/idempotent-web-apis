using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Shared.CommonDtoValidation;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder ProducedBadRequestProblemDetails(this RouteHandlerBuilder builder) =>
        builder.Produces<BadRequestProblemDetails>(StatusCodes.Status400BadRequest);
}