using Converter.Application.Models.Converter;
using Converter.Domain.Constants;
using FluentValidation;

namespace Converter.Application.Validators.Converter;

public class ConvertRequestValidator : AbstractValidator<ConvertRequest>
{
    private readonly string codeErrorMessage = $"Code must be {ConverterConstants.CodeLength} characters";
    
    public ConvertRequestValidator()
    {
        RuleFor(e => e.From)
            .Length(ConverterConstants.CodeLength)
            .WithMessage(codeErrorMessage);

        RuleFor(e => e.To)
            .Length(ConverterConstants.CodeLength)
            .WithMessage(codeErrorMessage);

        RuleFor(e => e.Qnt)
            .GreaterThan(decimal.Zero)
            .WithMessage("Quantity must be greater than zero");
    }
}
