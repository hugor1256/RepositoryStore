using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;
using RepositoryStore.Repositories.Abstractions;

namespace RepositoryStore.Repositories;

public abstract class Repository<T>(DbContext context) : IRepository<T> where T : class
{
    
    private readonly DbSet<T> _dbSet = context.Set<T>();
    
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    => await _dbSet.FindAsync(id, cancellationToken);

    public async Task<List<T>?> GetAllAsync(CancellationToken cancellationToken = default)
    => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        
}