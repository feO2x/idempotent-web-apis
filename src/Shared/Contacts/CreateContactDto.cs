using System.ComponentModel;

namespace Shared.Contacts;

public sealed record CreateContactDto : IContactProperties
{
    [Description("Name of the contact - must be between 1 and 100 characters")]
    public required string Name { get; set; }

    [Description("Email address of the contact - when provided, must be between 1 and 100 characters")]
    public string? Email { get; set; }

    [Description("Email address of the contact - when provided, must be between 1 and 100 characters")]
    public string? PhoneNumber { get; set; }
}
