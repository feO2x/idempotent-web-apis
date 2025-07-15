using Light.Validation;
using ServiceA.HttpAccess;
using Shared.Contacts;

namespace ServiceA.Contacts.CreateContact;

public sealed class CreateContactDtoWithErrorsValidator : Validator<CreateContactDtoWithErrors>
{
    public CreateContactDtoWithErrorsValidator(IValidationContextFactory validationContextFactory)
        : base(validationContextFactory) { }

    protected override CreateContactDtoWithErrors PerformValidation(
        ValidationContext context,
        CreateContactDtoWithErrors dto
    )
    {
        context.ValidateContactProperties(dto);
        context.ValidateErrorParameters(dto);
        return dto;
    }
}
