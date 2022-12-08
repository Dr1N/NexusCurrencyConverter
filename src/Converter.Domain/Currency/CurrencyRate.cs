using System.Collections.Generic;
using System.Linq;
using Converter.Domain.Base;
using Converter.Domain.Exceptions;

namespace Converter.Domain.Currency;

/// <summary>
/// Currency rate for currency pair
/// </summary>
public class CurrencyRate : ValueObject
{
    /// <summary>
    /// Source currency
    /// </summary>
    public Currency Source { get; }
        
    /// <summary>
    /// Destination currency
    /// </summary>
    public Currency Destination { get; }
        
    /// <summary>
    /// Rate source currency to destination currency
    /// </summary>
    public decimal Rate { get; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="source">Source currency <see cref="Currency"/></param>
    /// <param name="destination">Destination currency <see cref="Currency"/></param>
    /// <param name="rate">Rate source currency relative destination currency</param>
    /// <exception cref="DomainException"></exception>
    public CurrencyRate(Currency source, Currency destination, decimal rate)
    {
        if (rate <= decimal.Zero)
        {
            throw new DomainException($"{nameof(rate)} can't be less or equal zero");
        }

        if (source.Equals(destination))
        {
            throw new DomainException("Can't create rate for equal currencies");
        }
            
        Source = source;
        Destination = destination;
        Rate = rate;
    }

    public override string ToString() => $"{Source.Code} = {Rate} ${Destination.Code}";

    protected override IEnumerable<object> GetEqualityComponents()
        => new[] { Source, Destination }.OrderBy(e => e.Code);
}