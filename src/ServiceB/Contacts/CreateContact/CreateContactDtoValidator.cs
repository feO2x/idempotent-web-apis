using Light.Validation;
using Shared.Contacts;
using Shared.Model;

namespace ServiceB.Contacts.CreateContact;

public sealed class CreateContactDtoValidator : Validator<CreateContactDto>
{
    public CreateContactDtoValidator(IValidationContextFactory validationContextFactory)
        : base(validationContextFactory) { }

    protected override CreateContactDto PerformValidation(ValidationContext context, CreateContactDto dto)
    {
        context.ValidateContactProperties(dto);
        return dto;
    }
}
