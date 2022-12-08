using System.Linq.Expressions;
using Converter.Application.Contracts;
using Converter.Domain.Constants;
using Converter.Domain.Currency;
using Converter.Domain.Exceptions;
using Converter.Domain.Localization;

namespace Converter.Application.Services.Converter;

/// <inheritdoc />
public class CurrencyConverterService : ICurrencyConverterService
{
    private readonly IRepository<CurrencyRate> rateRepository;
    private readonly IRepository<ResultTemplate> templateRepository;

    public CurrencyConverterService(
        IRepository<CurrencyRate> rateRepository,
        IRepository<ResultTemplate> templateRepository)
    {
        this.rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
        this.templateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
    }

    /// <inheritdoc />
    public async Task<string> ConvertCurrencyAsync(
        string fromCode, 
        string toCode, 
        decimal quantity, 
        CancellationToken cancellationToken = default)
    {
        if (quantity < decimal.Zero)
        {
            throw new ArgumentException($"{nameof(quantity)} must be more than zero");
        }

        try
        {
            var currencyFrom = new Currency(fromCode);
            var currencyTo = new Currency(toCode);
            var rate = await GetCurrencyRateAsync(currencyFrom, currencyTo, cancellationToken);
            var template = await GetResultTemplateAsync(currencyFrom, cancellationToken);

            return MakeResult(currencyFrom, currencyTo, quantity, rate, template);
        }
        catch (DomainException dex)
        {
            throw new ApplicationException("Can't convert currencies", dex);
        }
    }

    private async Task<decimal> GetCurrencyRateAsync(
        Currency currencyFrom,
        Currency currencyTo,
        CancellationToken cancellationToken)
    {
        if (currencyFrom == currencyTo)
        {
            return decimal.One;
        }

        Expression<Func<CurrencyRate, bool>> predicate = 
            e => (e.Source == currencyFrom && e.Destination == currencyTo)
                || (e.Source == currencyTo && e.Destination == currencyFrom);
        
        var rate = await rateRepository.GetAsync(predicate, cancellationToken);
        if (rate is null)
        {
            throw new ApplicationException($"Conversion {currencyFrom.Code} to {currencyTo.Code} not supported");
        }

        return rate.Source == currencyFrom
            ? rate.Rate
            : decimal.One / rate.Rate;
    }

    private async Task<string> GetResultTemplateAsync(
        Currency currency, 
        CancellationToken cancellationToken)
    {
        var currencyTemplate = await templateRepository.GetAsync(
            e => e.Currency == currency, cancellationToken);

        return currencyTemplate is not null
            ? currencyTemplate.MessageTemplate
            : ConverterConstants.DefaultResultTemplate;
    }

    private static string MakeResult(
        Currency currencyFrom,
        Currency currencyTo,
        decimal quantity,
        decimal rate,
        string template)
    {
        var convertedQuantity = Math.Round(quantity * rate, 2);
        string result;
        try
        {
            result = string.Format(template, quantity, currencyFrom.Code, convertedQuantity, currencyTo.Code);
        }
        catch (FormatException)
        {
            result = string.Format(
                ConverterConstants.DefaultResultTemplate,
                quantity,
                currencyFrom.Code,
                convertedQuantity,
                currencyTo.Code);
        }

        return result;
    }
}
