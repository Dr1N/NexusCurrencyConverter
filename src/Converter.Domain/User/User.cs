using Converter.Domain.Constants;
using Converter.Domain.Exceptions;

namespace Converter.Domain.User;

/// <summary>
/// Api user
/// </summary>
public class User
{
    /// <summary>
    /// Login for authorisation
    /// </summary>
    public string Login { get; }
        
    /// <summary>
    /// Password for authorisation
    /// </summary>
    public string Password { get; }
        
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="login">User login</param>
    /// <param name="password">User password</param>
    /// <exception cref="DomainException"></exception>
    public User(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) || login.Length < ConverterConstants.MinLoginLength)
        {
            throw new DomainException($"Invalid {nameof(login)} value, " +
                                      $"{nameof(login)} must be more {ConverterConstants.MinLoginLength} characters");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < ConverterConstants.MinPasswordLength)
        {
            throw new DomainException($"Invalid {nameof(password)} value, " +
                                      $"{nameof(password)} must be more {ConverterConstants.MinPasswordLength} characters");
        }
            
        Login = login;
        Password = password;
    }
}
