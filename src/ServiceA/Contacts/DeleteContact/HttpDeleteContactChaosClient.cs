using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ServiceA.HttpAccess;

namespace ServiceA.Contacts.DeleteContact;

public sealed class HttpDeleteContactChaosClient : HttpChaosClient, IDeleteContactClient
{
    public HttpDeleteContactChaosClient(
        HttpClient client,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) : base(client, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall) { }

    public async Task<IResult> DeleteContactAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ThrowBeforeHttpCallIfNecessary();

        using var response = await Client.DeleteAsync(
            $"/api/contacts/{id}",
            cancellationToken
        );
        response.EnsureSuccessStatusCode();
        var result = TypedResults.NoContent();

        ThrowAfterHttpCallIfNecessary();

        return result;
    }

    public static IDeleteContactClient Create(
        HttpClient client,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) =>
        new HttpDeleteContactChaosClient(client, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall);
}
