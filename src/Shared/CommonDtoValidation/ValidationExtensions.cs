using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Light.GuardClauses;
using Light.Validation;
using Light.Validation.Checks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Range = Light.Validation.Tools.Range;

namespace Shared.CommonDtoValidation;

public static class ValidationExtensions
{
    public static bool CheckForErrors<T>(this Validator<T> validator, T value, [NotNullWhen(true)] out IResult? result)
    {
        if (validator.CheckForErrors(value, out var errors))
        {
            result = CreateBadRequest(errors);
            return true;
        }

        result = null;
        return false;
    }

    public static bool CheckIdForErrors(
        this ValidationContext validationContext,
        Guid id,
        [NotNullWhen(true)] out IResult? result
    )
    {
        validationContext.Check(id).IsNotEmpty();
        if (validationContext.TryGetErrors(out var errors))
        {
            result = CreateBadRequest(errors);
            return true;
        }

        result = null;
        return false;
    }

    public static void ValidatePagingParameters(this ValidationContext context, PagingParameters dto)
    {
        context.Check(dto.PageSize).IsIn(Range.FromInclusive(1).ToInclusive(100));
        if (dto.LastKnownId.HasValue && dto.LastKnownId.Value.IsEmpty())
        {
            context.AddError(nameof(dto.LastKnownId), "When specified, lastKnownId must be greater than 0");
        }
    }

    private static BadRequest<BadRequestProblemDetails> CreateBadRequest(object errors)
    {
        var errorsDictionary = Unsafe.As<Dictionary<string, string>>(errors);
        return TypedResults.BadRequest(new BadRequestProblemDetails(errorsDictionary));
    }
}
