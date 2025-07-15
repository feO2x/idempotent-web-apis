using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ServiceA.HttpAccess;
using Shared.Contacts;

namespace ServiceA.Contacts.GetContacts;

public sealed class HttpGetContactsChaosClient : HttpChaosClient, IGetContactsClient
{
    public HttpGetContactsChaosClient(
        HttpClient client,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) : base(client, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall) { }

    public async Task<List<ContactListDto>> GetContactsAsync(
        int pageSize,
        Guid? lastKnownId = null,
        CancellationToken cancellationToken = default
    )
    {
        ThrowBeforeHttpCallIfNecessary();

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["pageSize"] = pageSize.ToString();
        if (lastKnownId.HasValue)
        {
            query["lastKnownId"] = lastKnownId.Value.ToString();
        }

        var relativeUrl = $"/api/contacts?{query}";
        var contacts = await Client.GetFromJsonAsync<List<ContactListDto>>(
            relativeUrl,
            JsonSerializerOptions,
            cancellationToken
        );

        ThrowAfterHttpCallIfNecessary();

        return contacts ?? [];
    }

    public static IGetContactsClient Create(
        HttpClient client,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) =>
        new HttpGetContactsChaosClient(client, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall);
}
