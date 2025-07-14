namespace Shared.Model;

public sealed record CreateContactDto
{
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
