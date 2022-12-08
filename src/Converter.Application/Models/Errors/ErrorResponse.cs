// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Converter.Application.Models.Errors;

/// <summary>
/// Response with errors
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// List of errors
    /// </summary>
    public IEnumerable<string> Errors { get; set; }
}
