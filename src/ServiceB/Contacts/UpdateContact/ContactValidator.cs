using Light.Validation;
using Light.Validation.Checks;
using ServiceB.Contacts.Shared;
using Shared.Model;

namespace ServiceB.Contacts.UpdateContact;

public sealed class ContactValidator : Validator<Contact>
{
    public ContactValidator(IValidationContextFactory validationContextFactory) : base(validationContextFactory) { }

    protected override Contact PerformValidation(ValidationContext context, Contact dto)
    {
        context.Check(dto.Id).IsGreaterThan(0);
        context.ValidateContactProperties(dto);
        return dto;
    }
}
