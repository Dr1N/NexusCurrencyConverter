using Converter.Application.Models.Errors;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Converter.Api.Controllers;

[Authorize]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected static ErrorResponse ResponseFromValidationResult(ValidationResult validationResult)
    {
        if (validationResult is null)
        {
            throw new ArgumentNullException(nameof(validationResult));
        }

        var errors = validationResult.Errors
            .Select(e => e.ErrorMessage)
            .ToList();
        
        return new ErrorResponse { Errors = errors };
    }
}
