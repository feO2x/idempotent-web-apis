using Light.Validation;
using ServiceA.HttpAccess;
using Shared.Contacts;

namespace ServiceA.Contacts.UpdateContact;

public sealed class UpdateContactWithErrorsValidator : Validator<ErrorsDto<Contact>>
{
    private readonly ContactValidator _validator;

    public UpdateContactWithErrorsValidator(
        IValidationContextFactory validationContextFactory,
        ContactValidator validator
    ) : base(validationContextFactory) => _validator = validator;

    protected override ErrorsDto<Contact> PerformValidation(ValidationContext context, ErrorsDto<Contact> dto)
    {
        _validator.Validate(dto.OtherValues, context);
        context.ValidateErrorParameters(dto);
        return dto;
    }
}
