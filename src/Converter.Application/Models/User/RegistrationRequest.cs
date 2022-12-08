// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Converter.Application.Models.User;

/// <summary>
/// Request for registration new user
/// </summary>
public class RegistrationRequest
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
