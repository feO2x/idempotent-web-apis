using Light.Validation;
using ServiceA.HttpAccess;
using Shared.Contacts;

namespace ServiceA.Contacts.CreateContact;

public sealed class CreateContactDtoWithErrorsValidator : Validator<ErrorsDto<CreateContactDto>>
{
    public CreateContactDtoWithErrorsValidator(IValidationContextFactory validationContextFactory)
        : base(validationContextFactory) { }

    protected override ErrorsDto<CreateContactDto> PerformValidation(
        ValidationContext context,
        ErrorsDto<CreateContactDto> dto
    )
    {
        context.ValidateContactProperties(dto.OtherValues);
        context.ValidateErrorParameters(dto);
        return dto;
    }
}
