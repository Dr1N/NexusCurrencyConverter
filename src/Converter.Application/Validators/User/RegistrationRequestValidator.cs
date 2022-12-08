using Converter.Application.Models.User;
using Converter.Domain.Constants;
using FluentValidation;

namespace Converter.Application.Validators.User;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(e => e.Login)
            .MinimumLength(ConverterConstants.MinLoginLength)
            .WithMessage($"Login must contain at least {ConverterConstants.MinLoginLength} characters");
        
        RuleFor(e => e.Password)
            .MinimumLength(ConverterConstants.MinPasswordLength)
            .WithMessage($"Password must contain at least {ConverterConstants.MinPasswordLength} characters");
    }
}