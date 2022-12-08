using Microsoft.IdentityModel.Tokens;

namespace Converter.Application.Contracts;

/// <summary>
/// Service for work with JWT tokens
/// </summary>
public interface IJwtHandler
{
    /// <summary>
    /// Generate JWT token for user
    /// </summary>
    /// <param name="userName"></param>
    /// <returns>JWT token</returns>
    string GenerateToken(string userName);
    
    /// <summary>
    /// Token validation parameters
    /// </summary>
    TokenValidationParameters TokenValidationParameters { get; }    
}
