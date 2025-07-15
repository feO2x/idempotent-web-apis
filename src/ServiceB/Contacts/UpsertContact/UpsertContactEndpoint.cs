using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using Shared.CommonDtoValidation;
using Shared.Contacts;

namespace ServiceB.Contacts.UpsertContact;

public static class UpsertContactEndpoint
{
    public static void MapUpsertContactEndpoint(this WebApplication app)
    {
        app.MapPut("/api/contacts", UpsertContact)
           .WithName("UpsertContact")
           .WithTags("Contacts")
           .WithSummary("UpsertContact")
           .WithDescription("Creates or updates a contact")
           .Produces<Contact>()
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> UpsertContact(
        Contact dto,
        ContactValidator validator,
        IUpsertContactClient dbClient,
        ILogger logger,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(dto, out IResult? result))
        {
            return result;
        }

        var upsertResult = await dbClient.UpsertContactAsync(dto, cancellationToken);
        if (upsertResult == UpsertResult.Inserted)
        {
            logger.Information("Created contact {@Contact}", dto);
        }
        else
        {
            logger.Information("Updated contact {@Contact}", dto);
        }

        return TypedResults.Ok(dto);
    }
}
