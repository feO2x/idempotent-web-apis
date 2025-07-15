using Light.Validation;
using Light.Validation.Checks;

namespace Shared.Contacts;

public sealed class ContactValidator : Validator<Contact>
{
    public ContactValidator(IValidationContextFactory validationContextFactory) : base(validationContextFactory) { }

    protected override Contact PerformValidation(ValidationContext context, Contact dto)
    {
        context.Check(dto.Id).IsNotEmpty();
        context.ValidateContactProperties(dto);
        return dto;
    }
}
