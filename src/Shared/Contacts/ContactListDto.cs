namespace Shared.Contacts;

// ReSharper disable NotAccessedPositionalProperty.Global -- required for serialization
public sealed record ContactListDto(int Id, string Name);
// ReSharper restore NotAccessedPositionalProperty.Global