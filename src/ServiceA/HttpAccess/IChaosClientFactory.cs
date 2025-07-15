namespace ServiceA.HttpAccess;

public interface IChaosClientFactory<out T>
{
    T CreateClient(int numberOfErrorsBeforeServiceCall, int numberOfErrorsAfterServiceCall);
}
