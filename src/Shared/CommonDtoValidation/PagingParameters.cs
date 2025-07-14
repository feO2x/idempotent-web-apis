namespace Shared.CommonDtoValidation;

public readonly record struct PagingParameters(int PageSize, int? LastKnownId);