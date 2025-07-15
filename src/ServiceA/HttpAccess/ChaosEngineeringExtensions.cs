using System.IO;

namespace ServiceA.HttpAccess;

public static class ChaosEngineeringExtensions
{
    public static void ThrowBeforeHttpCallIfNecessary(this ref int numberOfErrorsBeforeServiceCall)
    {
        if (numberOfErrorsBeforeServiceCall <= 0)
        {
            return;
        }

        numberOfErrorsBeforeServiceCall--;
        throw new IOException("Simulated error before service call");
    }

    public static void ThrowAfterHttpCallIfNecessary(this ref int numberOfErrorsAfterServiceCall)
    {
        if (numberOfErrorsAfterServiceCall <= 0)
        {
            return;
        }

        numberOfErrorsAfterServiceCall--;
        throw new IOException("Simulated error after service call");
    }
}
