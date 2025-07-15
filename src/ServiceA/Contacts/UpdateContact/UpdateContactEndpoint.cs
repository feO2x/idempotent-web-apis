using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ServiceA.HttpAccess;
using Shared.CommonDtoValidation;
using Shared.Contacts;

namespace ServiceA.Contacts.UpdateContact;

public static class UpdateContactEndpoint
{
    public static void MapUpdateContactEndpoint(this WebApplication app) =>
        app.MapPut("/api/contacts/", UpdateContact)
           .WithName("UpdateContact")
           .WithTags("Contacts")
           .WithSummary("UpdateContact")
           .WithDescription("Updates a contact")
           .Produces(StatusCodes.Status204NoContent)
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status500InternalServerError);

    public static async Task<IResult> UpdateContact(
        UpdateContactWithErrorsValidator validator,
        IChaosClientFactory<IUpdateContactClient> clientFactory,
        Contact dto,
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
        var errorsDto = new ErrorsDto<Contact>(dto, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall);
        if (validator.CheckForErrors(errorsDto, out IResult? result))
        {
            return result;
        }

        await using var client = clientFactory.CreateClient(
            numberOfErrorsBeforeServiceCall,
            numberOfErrorsAfterServiceCall
        );
        return await client.UpdateContactAsync(dto, cancellationToken);
    }
}
