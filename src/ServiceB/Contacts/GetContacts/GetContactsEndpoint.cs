using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.CommonDtoValidation;
using Shared.Contacts;

namespace ServiceB.Contacts.GetContacts;

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
        IGetContactsClient dbClient,
        PagingValidator validator,
        [Description("Number of contacts to return (optional) - between 1 and 100")]
        int pageSize = 20,
        [Description("ID of the last known contact to the caller (optional) - page will start after this ID")]
        int? lastKnownId = null,
        CancellationToken cancellationToken = default
    )
    {
        if (validator.CheckForErrors(new (pageSize, lastKnownId), out IResult? result))
        {
            return result;
        }

        var contacts = await dbClient.GetContactsAsync(pageSize, lastKnownId, cancellationToken);
        return TypedResults.Ok(contacts);
    }
}