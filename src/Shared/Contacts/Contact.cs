using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shared.Contacts;

public sealed class Contact : IContactProperties
{
    public int Id { get; set; }

    [Description("Name of the contact - must be between 1 and 100 characters")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Description("Email address of the contact - when provided, must be between 1 and 100 characters")]
    public string? Email { get; set; }

    [Description("Email address of the contact - when provided, must be between 1 and 100 characters")]
    public string? PhoneNumber { get; set; }
}
