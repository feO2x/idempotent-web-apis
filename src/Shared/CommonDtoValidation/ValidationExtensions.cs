using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Light.Validation;
using Light.Validation.Checks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

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
        int id,
        [NotNullWhen(true)] out IResult? result
    )
    {
        validationContext.Check(id).IsGreaterThan(0);
        if (validationContext.TryGetErrors(out var errors))
        {
            result = CreateBadRequest(errors);
            return true;
        }

        result = null;
        return false;
    }

    private static BadRequest<BadRequestProblemDetails> CreateBadRequest(object errors)
    {
        var errorsDictionary = Unsafe.As<Dictionary<string, string>>(errors);
        return TypedResults.BadRequest(new BadRequestProblemDetails(errorsDictionary));
    }
}
