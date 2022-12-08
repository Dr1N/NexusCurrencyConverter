using Converter.Application.Models.Converter;
using Converter.Application.Services.Converter;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Converter.Api.Controllers;

[Route("[controller]")]
public class CurrencyController : ApiControllerBase
{
    private readonly ICurrencyConverterService converterService;
    private readonly IValidator<ConvertRequest> validator;

    public CurrencyController(ICurrencyConverterService converterService, IValidator<ConvertRequest> validator)
    {
        this.converterService = converterService ?? throw new ArgumentNullException(nameof(converterService));
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    [HttpGet]
    [Route("convert")]
    public async Task<IActionResult> ConvertAsync(
        [FromQuery] ConvertRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(ResponseFromValidationResult(validationResult));
        }

        var currencyConvert = await converterService.ConvertCurrencyAsync(
            request.From,
            request.To,
            request.Qnt,
            cancellationToken);
   
        return Ok(new ConvertResponse { Result = currencyConvert });
    } 
}
