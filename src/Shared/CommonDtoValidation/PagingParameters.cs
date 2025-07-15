using System;

namespace Shared.CommonDtoValidation;

public readonly record struct PagingParameters(int PageSize, Guid? LastKnownId);