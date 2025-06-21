using System.Collections.Generic;
using Light.GuardClauses;
using Light.SharedCore.Entities;

namespace WebApi.DatabaseAccess.Model;

public sealed class Contact : IEntity<int>
{
    private List<Address>? _addresses;
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;

    public List<Address> Addresses
    {
        get => _addresses ??= [];
        set => _addresses = value.MustNotBeNull();
    }
}
