namespace ServiceA.HttpAccess;

public interface IErrorsDto
{
    int NumberOfErrorsBeforeServiceCall { get; }
    int NumberOfErrorsAfterServiceCall { get; }
}
