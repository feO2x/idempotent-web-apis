using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ServiceA.HttpAccess;
using Shared.CommonDtoValidation;
using Shared.Contacts;

namespace ServiceA.Contacts.CreateContact;

public static class CreateContactEndpoint
{
    public static void MapCreateContactEndpoint(this WebApplication app) =>
        app.MapPost("/api/contacts", CreateContact)
           .WithName("CreateContact")
           .WithTags("Contacts")
           .WithSummary("CreateContact")
           .WithDescription("Creates a new contact")
           .Produces<Contact>(StatusCodes.Status201Created)
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status500InternalServerError);

    public static async Task<IResult> CreateContact(
        CreateContactDtoWithErrorsValidator validator,
        IChaosClientFactory<ICreateContactClient> clientFactory,
        CreateContactDto dto,
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
        var errorsDto = new ErrorsDto<CreateContactDto>(
            dto,
            numberOfErrorsBeforeServiceCall,
            numberOfErrorsAfterServiceCall
        );
        if (validator.CheckForErrors(errorsDto, out IResult? result))
        {
            return result;
        }

        await using var client = clientFactory.CreateClient(
            numberOfErrorsBeforeServiceCall,
            numberOfErrorsAfterServiceCall
        );
        var contact = await client.CreateContactAsync(dto, cancellationToken);
        return TypedResults.Created($"/api/contacts/{contact.Id}", contact);
    }
}
