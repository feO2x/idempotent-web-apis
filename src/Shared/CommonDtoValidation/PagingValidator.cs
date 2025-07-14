using Light.Validation;
using Light.Validation.Checks;
using Light.Validation.Tools;

namespace Shared.CommonDtoValidation;

public sealed class PagingValidator : Validator<PagingParameters>
{
    public PagingValidator(IValidationContextFactory validationContextFactory) : base(validationContextFactory) { }

    protected override PagingParameters PerformValidation(ValidationContext context, PagingParameters value)
    {
        context.Check(value.PageSize).IsIn(Range.FromInclusive(1).ToInclusive(100));
        if (value.LastKnownId is <= 0)
        {
            context.AddError(nameof(value.LastKnownId), "When specified, lastKnownId must be greater than 0");
        }

        return value;
    }
}