using System.Collections.Generic;
using Converter.Domain.Base;
using Converter.Domain.Exceptions;

namespace Converter.Domain.Localization;

/// <summary>
/// Template for result message
/// </summary>
public class ResultTemplate : ValueObject
{
    /// <summary>
    /// Source currency
    /// </summary>
    public Currency.Currency Currency { get; }
        
    /// <summary>
    /// Result template for currency
    /// </summary>
    public string MessageTemplate { get; }
        
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="currency">Target currency <see cref="Currency"/></param>
    /// <param name="messageTemplate">Message template for currency on currency country language</param>
    /// <exception cref="DomainException"></exception>
    /// <remarks>Template example for english: "For {0} {1} you will get {1} {2}}"</remarks>
    public ResultTemplate(Currency.Currency currency, string messageTemplate)
    {
        if (string.IsNullOrWhiteSpace(messageTemplate))
        {
            throw new DomainException($"{nameof(messageTemplate)} can't be empty");
        }

        Currency = currency ?? throw new DomainException($"{nameof(currency)} can't be null");
        MessageTemplate = messageTemplate;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency;
    }
}