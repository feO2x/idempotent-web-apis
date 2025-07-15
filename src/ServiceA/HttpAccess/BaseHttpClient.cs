using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceA.HttpAccess;

public abstract class BaseHttpClient : IAsyncDisposable
{
    protected BaseHttpClient(HttpClient client) => Client = client;

    public static JsonSerializerOptions JsonSerializerOptions { get; } = new (JsonSerializerDefaults.Web);

    public HttpClient Client { get; }

    public ValueTask DisposeAsync()
    {
        Client.Dispose();
        return ValueTask.CompletedTask;
    }
}
