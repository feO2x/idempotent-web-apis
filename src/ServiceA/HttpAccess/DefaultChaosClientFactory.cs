using System.Net.Http;

namespace ServiceA.HttpAccess;

public sealed class DefaultChaosClientFactory<T> : IChaosClientFactory<T>
{
    private readonly CreateChaosClient<T> _createChaosClient;
    private readonly IHttpClientFactory _httpClientFactory;

    public DefaultChaosClientFactory(
        IHttpClientFactory httpClientFactory,
        CreateChaosClient<T> createChaosClient
    )
    {
        _httpClientFactory = httpClientFactory;
        _createChaosClient = createChaosClient;
    }

    public T CreateClient(int numberOfErrorsBeforeServiceCall, int numberOfErrorsAfterServiceCall)
    {
        var httpClient = _httpClientFactory.CreateClient("service-b-client");
        return _createChaosClient(httpClient, numberOfErrorsBeforeServiceCall, numberOfErrorsAfterServiceCall);
    }
}
