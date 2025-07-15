namespace Shared.Contacts;

public abstract record BaseCreateContactDto : IContactProperties
{
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
