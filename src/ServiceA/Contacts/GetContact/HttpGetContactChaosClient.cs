using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using ServiceA.HttpAccess;
using Shared.Contacts;
using Shared.Model;

namespace ServiceA.Contacts.GetContact;

public sealed class HttpGetContactChaosClient : HttpChaosClient, IGetContactClient
{
    public HttpGetContactChaosClient(
        HttpClient client,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) : base(client, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall) { }


    public Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken = default)
    {
        ThrowBeforeHttpCallIfNecessary();

        var contact = Client.GetFromJsonAsync<Contact?>(
            $"/api/contacts/{id}",
            JsonSerializerOptions,
            cancellationToken
        );

        ThrowAfterHttpCallIfNecessary();

        return contact;
    }

    public static IGetContactClient Create(
        HttpClient httpClient,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) =>
        new HttpGetContactChaosClient(httpClient, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall);
}
