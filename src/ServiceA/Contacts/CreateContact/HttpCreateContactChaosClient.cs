using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Light.GuardClauses;
using ServiceA.HttpAccess;
using Shared.Contacts;

namespace ServiceA.Contacts.CreateContact;

public sealed class HttpCreateContactChaosClient : HttpChaosClient, ICreateContactClient
{
    public HttpCreateContactChaosClient(
        HttpClient client,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) : base(client, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall) { }

    public async Task<Contact> CreateContactAsync(Contact dto, CancellationToken cancellationToken = default)
    {
        ThrowBeforeHttpCallIfNecessary();

        using var response = await Client.PutAsJsonAsync(
            "/api/contacts",
            dto,
            JsonSerializerOptions,
            cancellationToken
        );
        response.EnsureSuccessStatusCode();
        var contact = await response.Content.ReadFromJsonAsync<Contact>(JsonSerializerOptions, cancellationToken);

        ThrowAfterHttpCallIfNecessary();

        return contact.MustNotBeNull(
            () => throw new InvalidDataException("Service B returned null which violates the contract")
        );
    }

    public static ICreateContactClient Create(
        HttpClient httpClient,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) =>
        new HttpCreateContactChaosClient(httpClient, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall);
}
