// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Converter.Application.Models.User;

/// <summary>
/// Sign-in request
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// User login
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// User password
    /// </summary>
    public string Password { get; set; }
}