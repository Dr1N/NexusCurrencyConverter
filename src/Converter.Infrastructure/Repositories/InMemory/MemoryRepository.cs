using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Converter.Application.Contracts;

namespace Converter.Infrastructure.Repositories.InMemory;

/// <inheritdoc />
public sealed class MemoryRepository<T> : IRepository<T> where T: class
{
    private static readonly ConcurrentBag<T> Storage = new();  
    
    public Task<T> GetAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        var result = Storage
            .AsQueryable()
            .SingleOrDefault(predicate);
        
        return Task.FromResult(result);
    }

    public Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        if (predicate is null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }
        
        var result = Storage
            .AsQueryable()
            .Where(predicate)
            .ToList();
        
        return Task.FromResult<IEnumerable<T>>(result);
    }

    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        
        Storage.Add(entity);
        
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
}
