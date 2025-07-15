using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Light.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.CommonDtoValidation;
using Shared.Contacts;

namespace ServiceB.Contacts.GetContact;

public static class GetContactEndpoint
{
    public static void MapGetContactEndpoint(this WebApplication app) =>
        app.MapGet("/api/contacts/{id:int}", GetContact)
           .WithName("GetContact")
           .WithTags("Contacts")
           .WithSummary("GetContact")
           .WithDescription("Returns details for a single contact")
           .Produces<Contact>()
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status500InternalServerError);

    public static async Task<IResult> GetContact(
        ValidationContext validationContext,
        IGetContactClient dbClient,
        [Description("ID of the contact to return - must be greater than 0")] int id,
        CancellationToken cancellationToken = default
    )
    {
        if (validationContext.CheckIdForErrors(id, out var result))
        {
            return result;
        }

        var contact = await dbClient.GetContactAsync(id, cancellationToken);
        return contact is null ? TypedResults.NotFound() : TypedResults.Ok(contact);
    }
}
