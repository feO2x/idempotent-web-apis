using Light.Validation;
using Light.Validation.Checks;
using Light.Validation.Tools;

namespace Shared.Contacts;

public static class CommonValidation
{
    public static void ValidateContactProperties<T>(this ValidationContext context, T dto)
        where T : class, IContactProperties
    {
        dto.Name = context.Check(dto.Name).IsNotNullOrWhiteSpace().HasLengthIn(Range.FromInclusive(1).ToInclusive(100));
        if (dto.Email is not null)
        {
            dto.Email = context
               .Check(dto.Email)
               .HasLengthIn(Range.FromInclusive(1).ToInclusive(100));
        }

        if (dto.PhoneNumber is not null)
        {
            dto.PhoneNumber = context
               .Check(dto.PhoneNumber)
               .HasLengthIn(Range.FromInclusive(1).ToInclusive(100));
        }
    }
}
