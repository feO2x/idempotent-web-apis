using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ServiceA.HttpAccess;
using Shared.Contacts;

namespace ServiceA.Contacts.UpdateContact;

public sealed class HttpUpdateContactChaosClient : HttpChaosClient, IUpdateContactClient
{
    public HttpUpdateContactChaosClient(
        HttpClient client,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) : base(client, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall) { }

    public async Task<IResult> UpdateContactAsync(Contact contact, CancellationToken cancellationToken = default)
    {
        ThrowBeforeHttpCallIfNecessary();

        using var response = await Client.PutAsJsonAsync("/api/contacts", contact, cancellationToken);
        IResult result;
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            result = TypedResults.NotFound();
        }
        else
        {
            response.EnsureSuccessStatusCode();
            result = TypedResults.NoContent();
        }

        ThrowAfterHttpCallIfNecessary();

        return result;
    }

    public static IUpdateContactClient Create(
        HttpClient httpClient,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) =>
        new HttpUpdateContactChaosClient(httpClient, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall);
}
