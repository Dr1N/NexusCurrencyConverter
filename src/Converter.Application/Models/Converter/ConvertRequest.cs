// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Converter.Application.Models.Converter;

/// <summary>
/// Request for conversion currencies
/// </summary>
public class ConvertRequest
{
    /// <summary>
    /// Source currency ISO code
    /// </summary>
    public string From { get; set; }

    /// <summary>
    /// Destination currency ISO code
    /// </summary>
    public string To { get; set; }
    
    /// <summary>
    /// Source currency quantity
    /// </summary>
    public decimal Qnt { get; set; }
}