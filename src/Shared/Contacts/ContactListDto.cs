using System.ComponentModel;

namespace Shared.Contacts;

// ReSharper disable NotAccessedPositionalProperty.Global -- required for serialization
public sealed record ContactListDto(
    [Description("ID of the contact")] int Id,
    [Description("Name of the contact")] string Name
);
// ReSharper restore NotAccessedPositionalProperty.Global
