using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using Shared.CommonDtoValidation;
using Shared.Contacts;

namespace ServiceB.Contacts.CreateContact;

public static class CreateContactEndpoint
{
    public static void MapCreateContactEndpoint(this WebApplication app)
    {
        app.MapPost("/api/contacts", CreateContact)
           .WithName("CreateContact")
           .WithTags("Contacts")
           .WithSummary("CreateContact")
           .WithDescription("Creates a new contact")
           .Produces<Contact>()
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> CreateContact(
        Contact dto,
        ContactValidator validator,
        ICreateContactClient dbClient,
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
