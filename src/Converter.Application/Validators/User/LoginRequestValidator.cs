using Converter.Application.Models.User;
using Converter.Domain.Constants;
using FluentValidation;

namespace Converter.Application.Validators.User;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(e => e.Login)
            .MinimumLength(ConverterConstants.MinLoginLength)
            .WithMessage($"Invalid login format. Login must contain at least {ConverterConstants.MinLoginLength} characters");
        
        RuleFor(e => e.Password)
            .MinimumLength(ConverterConstants.MinPasswordLength)
            .WithMessage($"Invalid password format. Password must contain at least {ConverterConstants.MinPasswordLength} characters");
    }
}