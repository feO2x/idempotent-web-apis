using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Light.GuardClauses;
using Light.Validation;
using Microsoft.AspNetCore.Http;

namespace Shared.CommonDtoValidation;

public static class ValidationExtensions
{
    public static bool CheckForErrors<T>(this Validator<T> validator, T value, [NotNullWhen(true)] out IResult? result)
    {
        if (validator.CheckForErrors(value, out var errors))
        {
            var errorsDictionary = errors.MustBeOfType<Dictionary<string, string>>();
            result = TypedResults.BadRequest(new BadRequestProblemDetails(errorsDictionary));
            return true;
        }

        result = null;
        return false;
    }
}