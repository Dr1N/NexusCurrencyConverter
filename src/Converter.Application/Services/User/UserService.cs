using Converter.Application.Contracts;

namespace Converter.Application.Services.User;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IRepository<Domain.User.User> usersRepository;

    public UserService(IRepository<Domain.User.User> usersRepository)
        => this.usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));

    /// <inheritdoc />
    public async Task RegisterUser(Domain.User.User user, CancellationToken cancellationToken = default)
    {
        var existingUser = await usersRepository.GetAsync(e => e.Login == user.Login, cancellationToken);
        if (existingUser is not null)
        {
            throw new ApplicationException($"User with login {user.Login} already exists");
        }

        await usersRepository.AddAsync(user, cancellationToken);
        await usersRepository.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> IsExistsAsync(string login, string password, CancellationToken cancellationToken)
    {
        var existingUser = await usersRepository.GetAsync(
            e => e.Login == login && e.Password == password,
            cancellationToken);
        
        return existingUser is not null;
    }
}
