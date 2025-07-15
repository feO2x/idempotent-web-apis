using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ServiceA.HttpAccess;
using Shared.CommonDtoValidation;
using Shared.Contacts;
using Shared.Model;

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
        [Description("Number of contacts to return (optional) - between 1 and 100")] int pageSize = 20,
        [Description("ID of the last known contact to the caller (optional) - page will start after this ID")]
        int? lastKnownId = null,
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
        var contacts = await client.GetContactsAsync(pageSize, lastKnownId, cancellationToken);
        return TypedResults.Ok(contacts);
    }
}
