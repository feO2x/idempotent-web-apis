using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

// ReSharper disable UnusedAutoPropertyAccessor.Global -- required for serialization

namespace Shared.CommonDtoValidation;

public sealed class BadRequestProblemDetails
{
    public const string BadRequestType = "https://tools.ietf.org/html/rfc9110#section-15.5.1";
    public const string BadRequestTitle = "Bad Request";

    public BadRequestProblemDetails(Dictionary<string, string> errors)
    {
        Errors = errors;
        Status = StatusCodes.Status400BadRequest;
        Type = BadRequestType;
        Title = BadRequestTitle;
    }

    [Description("The errors found in the HTTP request")]
    public Dictionary<string, string> Errors { get; }

    [Description("Status code of the HTTP response")]
    [DefaultValue(StatusCodes.Status400BadRequest)]
    public int Status { get; init; }

    [Description("A link to the RFC descripting the problem")]
    [DefaultValue(BadRequestType)]
    [Required]
    public string Type { get; init; }

    [Description("The human-readable title of the problem type")]
    [DefaultValue(BadRequestTitle)]
    [Required]
    public string Title { get; init; }
}