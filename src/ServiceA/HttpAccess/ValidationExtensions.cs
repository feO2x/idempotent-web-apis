using Light.Validation;
using Light.Validation.Checks;

namespace ServiceA.HttpAccess;

public static class ValidationExtensions
{
    public static void ValidateErrorParameters<T>(this ValidationContext context, T dto)
        where T : IErrorsDto
    {
        context.Check(dto.NumberOfErrorsBeforeServiceCall).IsGreaterThanOrEqualTo(0);
        context.Check(dto.NumberOfErrorsAfterServiceCall).IsGreaterThanOrEqualTo(0);
    }
}
