namespace ServiceA.HttpAccess;

public readonly record struct ErrorsDto<T>(
    T OtherValues,
    int NumberOfErrorsBeforeServiceCall,
    int NumberOfErrorsAfterServiceCall
)
    : IErrorsDto;
