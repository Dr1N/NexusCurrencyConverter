namespace Converter.Application.Services.Converter;

/// <summary>
/// Conversion currency service
/// </summary>
public interface ICurrencyConverterService
{
    /// <summary>
    /// Convert currency from fromCode to toCode
    /// </summary>
    /// <param name="fromCode">Source currency code (ISO 4217)</param>
    /// <param name="toCode">Destination currency code (ISO 4217)</param>
    /// <param name="quantity">Quantity of source currency</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Message with result in source currency country language</returns>
    /// <remarks>If no translation on currency country language will be use english</remarks>
    Task<string> ConvertCurrencyAsync(
        string fromCode, 
        string toCode, 
        decimal quantity, 
        CancellationToken cancellationToken = default);
}