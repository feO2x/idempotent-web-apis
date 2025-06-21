using System;
using Light.SharedCore.Entities;

namespace WebApi.DatabaseAccess.Model;

public sealed class Address : IEntity<int>
{
    private Contact? _contact;
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public int ContactId { get; set; }

    public Contact Contact
    {
        get => _contact ?? throw new InvalidOperationException("Contact has not been loaded");
        set => _contact = value;
    }
}
