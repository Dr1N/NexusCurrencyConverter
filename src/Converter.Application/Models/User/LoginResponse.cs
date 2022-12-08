// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Converter.Application.Models.User;

/// <summary>
/// Sign-in user response with token
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// JWT token
    /// </summary>
    public string Token { get; set; }
}
