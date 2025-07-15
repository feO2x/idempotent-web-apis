using Light.Validation;

namespace Shared.CommonDtoValidation;

public sealed class PagingValidator : Validator<PagingParameters>
{
    public PagingValidator(IValidationContextFactory validationContextFactory) : base(validationContextFactory) { }

    protected override PagingParameters PerformValidation(ValidationContext context, PagingParameters dto)
    {
        context.ValidatePagingParameters(dto);
        return dto;
    }
}
