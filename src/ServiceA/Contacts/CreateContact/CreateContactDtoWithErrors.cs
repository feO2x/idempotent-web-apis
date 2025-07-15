using ServiceA.HttpAccess;
using Shared.Model;

namespace ServiceA.Contacts.CreateContact;

public sealed record CreateContactDtoWithErrors : BaseCreateContactDto, IErrorsDto
{
    public int NumberOfErrorsBeforeServiceCall { get; set; }
    public int NumberOfErrorsAfterServiceCall { get; set; }

    public CreateContactDto ToCreateContactDto() =>
        new ()
        {
            Name = Name,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
}
