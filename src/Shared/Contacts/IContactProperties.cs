namespace Shared.Contacts;

public interface IContactProperties
{
    string Name { get; set; }
    string? Email { get; set; }
    string? PhoneNumber { get; set; }
}
