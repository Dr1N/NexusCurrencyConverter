using System.Collections.Generic;
using Converter.Domain.Base;
using Converter.Domain.Constants;
using Converter.Domain.Exceptions;

namespace Converter.Domain.Currency;

/// <summary>
/// Currency model
/// </summary>
public class Currency : ValueObject
{
    /// <summary>
    /// Currency code (ISO 4217)
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="code">Currency code by ISO 4217 </param>
    /// <exception cref="DomainException"></exception>
    public Currency(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length != ConverterConstants.CodeLength)
        {
            throw new DomainException($"Invalid {nameof(code)} value, " +
                                      $"{nameof(code)} must be {ConverterConstants.CodeLength} characters");
        }

        Code = code.ToUpperInvariant();
    }

    public override string ToString() => Code;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}