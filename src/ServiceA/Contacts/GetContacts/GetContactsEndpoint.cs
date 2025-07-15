using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Polly;
using ServiceA.HttpAccess;
using Shared.CommonDtoValidation;
using Shared.Contacts;

namespace ServiceA.Contacts.GetContacts;

public static class GetContactsEndpoint
{
    public static void MapGetContactsEndpoint(this WebApplication app)
    {
        app.MapGet("/api/contacts", GetContacts)
           .WithName("GetContacts")
           .WithTags("Contacts")
           .WithSummary("GetContacts")
           .WithDescription("Returns a paged list of contacts")
           .Produces<List<ContactListDto>>()
           .ProducedBadRequestProblemDetails()
           .Produces(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> GetContacts(
        IChaosClientFactory<IGetContactsClient> clientFactory,
        GetContactsValidator validator,
        ResiliencePipeline resiliencePipeline,
        [Description("Number of contacts to return (optional) - between 1 and 100")] int pageSize = 20,
        [Description("ID of the last known contact to the caller (optional) - page will start after this ID")]
        Guid? lastKnownId = null,
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
        var errorsDto = new ErrorsDto<PagingParameters>(
            new (pageSize, lastKnownId),
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
        var contacts = await resiliencePipeline.ExecuteAsync(
            async (state, ct) => await state.Client.GetContactsAsync(state.PageSize, state.LastKnownId, ct),
            (Client: client, PageSize: pageSize, LastKnownId: lastKnownId),
            cancellationToken
        );
        return TypedResults.Ok(contacts);
    }
}
