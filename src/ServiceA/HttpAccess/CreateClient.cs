using System.Net.Http;

namespace ServiceA.HttpAccess;

public delegate T CreateChaosClient<out T>(
    HttpClient httpClient,
    int numberOfErrorsBeforeServiceCall,
    int numberOfErrorsAfterServiceCall
);
