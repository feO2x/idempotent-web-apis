using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ServiceA.Contacts.Shared;
using ServiceA.HttpAccess;
using Shared.CommonDtoValidation;
using Shared.Contacts;
using Shared.Model;

namespace ServiceA.Contacts.GetContact;

public static class GetContactEndpoint
{
    public static void MapGetContactEndpoint(this WebApplication app) =>
        app.MapGet("/api/contacts/{id}", GetContact)
           .WithName("GetContact")
           .WithTags("Contacts")
           .WithSummary("GetContact")
           .WithDescription("Returns details for a single contact")
           .Produces<Contact>()
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status500InternalServerError);

    public static async Task<IResult> GetContact(
        IChaosClientFactory<IGetContactClient> clientFactory,
        ContactIdWithErrorsValidator validator,
        [Description("ID of the contact to return - must be greater than 0.")] int id,
        [Description(
            "Number of errors that should occur before the HTTP call to Service B - must be greater than or equal to 0"
        )]
        int numberOfErrorsBeforeServiceCall = 0,
        [Description(
            "Number of errors that should occur after the HTTP call to Service B - must be greater than or equal to 0"
        )]
        int numberOfErrorsAfterServiceCall = 0,
        CancellationToken cancellationToken = default
    )
    {
        var dto = new ErrorsDto<int>(id, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall);
        if (validator.CheckForErrors(dto, out IResult? result))
        {
            return result;
        }

        await using var client = clientFactory.CreateClient(
            numberOfErrorsBeforeServiceCall,
            numberOfErrorsAfterServiceCall
        );
        var contact = await client.GetContactAsync(id, cancellationToken);
        return contact is null ? TypedResults.NotFound() : TypedResults.Ok(contact);
    }
}
