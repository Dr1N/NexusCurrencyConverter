using System.Linq.Expressions;

namespace Converter.Application.Contracts;

/// <summary>
/// Base repository contract
/// </summary>
/// <typeparam name="T">Type of entity</typeparam>
public interface IRepository<T> where T: class
{
    /// <summary>
    /// Get single entity by predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Entity or null</returns>
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities by predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>List of entities</returns>
    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Add entity to repository
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Commit repository changes (UnitOfWork)
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
