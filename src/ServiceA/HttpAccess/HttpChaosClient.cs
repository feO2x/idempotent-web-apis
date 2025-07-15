using System.Net.Http;

namespace ServiceA.HttpAccess;

public abstract class HttpChaosClient : BaseHttpClient
{
    private int _numberOfErrorsAfterServiceCall;

    private int _numberOfErrorsBeforeServiceCall;

    protected HttpChaosClient(
        HttpClient client,
        int numberOfErrorsBeforeServiceCall,
        int numberOfErrorsAfterServiceCall
    ) : base(client)
    {
        _numberOfErrorsBeforeServiceCall = numberOfErrorsBeforeServiceCall;
        _numberOfErrorsAfterServiceCall = numberOfErrorsAfterServiceCall;
    }

    protected void ThrowBeforeHttpCallIfNecessary() =>
        _numberOfErrorsBeforeServiceCall.ThrowBeforeHttpCallIfNecessary();

    protected void ThrowAfterHttpCallIfNecessary() =>
        _numberOfErrorsAfterServiceCall.ThrowAfterHttpCallIfNecessary();
}
