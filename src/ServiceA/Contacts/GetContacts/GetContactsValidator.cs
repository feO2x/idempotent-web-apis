using Light.Validation;
using ServiceA.HttpAccess;
using Shared.CommonDtoValidation;

namespace ServiceA.Contacts.GetContacts;

public sealed class GetContactsValidator : Validator<ErrorsDto<PagingParameters>>
{
    public GetContactsValidator(IValidationContextFactory validationContextFactory) : base(validationContextFactory) { }

    protected override ErrorsDto<PagingParameters> PerformValidation(
        ValidationContext context,
        ErrorsDto<PagingParameters> dto
    )
    {
        context.ValidateErrorParameters(dto);
        context.ValidatePagingParameters(dto.OtherValues);
        return dto;
    }
}
