namespace Converter.Application.Services.User;

/// <summary>
/// Service for base operation with users
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="user">New user <see cref="Domain.User.User"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RegisterUser(Domain.User.User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check existing user with login and password
    /// </summary>
    /// <param name="login">User login</param>
    /// <param name="password">User password</param>
    /// <param name="cancellationToken"></param>
    /// <returns>true - if user exist, otherwise false</returns>
    Task<bool> IsExistsAsync(string login, string password, CancellationToken cancellationToken = default);
}