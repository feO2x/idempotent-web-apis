using Light.Validation;
using Light.Validation.Checks;
using ServiceA.HttpAccess;

namespace ServiceA.Contacts.Shared;

public sealed class ContactIdWithErrorsValidator : Validator<ErrorsDto<int>>
{
    public ContactIdWithErrorsValidator(IValidationContextFactory validationContextFactory)
        : base(validationContextFactory) { }

    protected override ErrorsDto<int> PerformValidation(ValidationContext context, ErrorsDto<int> value)
    {
        context.ValidateErrorParameters(value);
        var id = value.OtherValues;
        context.Check(id).IsGreaterThan(0);
        return value;
    }
}
