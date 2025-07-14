using Light.Validation;
using Light.Validation.Checks;
using Light.Validation.Tools;
using Shared.Model;

namespace ServiceB.Contacts.CreateContact;

public sealed class CreateContactDtoValidator : Validator<CreateContactDto>
{
    public CreateContactDtoValidator(IValidationContextFactory validationContextFactory)
        : base(validationContextFactory) { }

    protected override CreateContactDto PerformValidation(ValidationContext context, CreateContactDto dto)
    {
        dto.Name = context
           .Check(dto.Name)
           .IsNotNullOrWhiteSpace()
           .HasLengthIn(Range.FromInclusive(1).ToInclusive(100));

        if (dto.Email is not null)
        {
            context
               .Check(dto.Email, normalizeValue: false)
               .HasLengthIn(Range.FromInclusive(1).ToInclusive(100));
        }

        if (dto.PhoneNumber is not null)
        {
            context
               .Check(dto.PhoneNumber, normalizeValue: false)
               .HasLengthIn(Range.FromInclusive(1).ToInclusive(100));
        }

        return dto;
    }
}
