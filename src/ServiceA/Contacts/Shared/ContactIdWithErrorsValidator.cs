using System;
using Light.Validation;
using Light.Validation.Checks;
using ServiceA.HttpAccess;

namespace ServiceA.Contacts.Shared;

public sealed class ContactIdWithErrorsValidator : Validator<ErrorsDto<Guid>>
{
    public ContactIdWithErrorsValidator(IValidationContextFactory validationContextFactory)
        : base(validationContextFactory) { }

    protected override ErrorsDto<Guid> PerformValidation(ValidationContext context, ErrorsDto<Guid> value)
    {
        context.ValidateErrorParameters(value);
        var id = value.OtherValues;
        context.Check(id).IsNotEmpty();
        return value;
    }
}
