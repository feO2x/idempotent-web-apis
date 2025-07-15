using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Polly;
using ServiceA.Contacts.Shared;
using ServiceA.HttpAccess;
using Shared.CommonDtoValidation;

namespace ServiceA.Contacts.DeleteContact;

public static class DeleteContactEndpoint
{
    public static void MapDeleteContactEndpoint(this WebApplication app) =>
        app.MapDelete("/api/contacts/{id:guid}", DeleteContact)
           .WithName("DeleteContact")
           .WithTags("Contacts")
           .WithSummary("DeleteContact")
           .WithDescription("Deletes a contact")
           .Produces(StatusCodes.Status204NoContent)
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status500InternalServerError);

    public static async Task<IResult> DeleteContact(
        ContactIdWithErrorsValidator validator,
        IChaosClientFactory<IDeleteContactClient> clientFactory,
        ResiliencePipeline resiliencePipeline,
        [Description("ID of the contact to delete - must not be empty")] Guid id,
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
        var dto = new ErrorsDto<Guid>(id, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall);
        if (validator.CheckForErrors(dto, out IResult? result))
        {
            return result;
        }

        await using var client = clientFactory.CreateClient(
            numberOfErrorsBeforeServiceCall,
            numberOfErrorsAfterServiceCall
        );

        return await resiliencePipeline.ExecuteAsync(
            async (state, ct) => await state.Client.DeleteContactAsync(state.Id, ct),
            (Client: client, Id: id),
            cancellationToken
        );
    }
}
