using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;
using RepositoryStore.Models;
using RepositoryStore.Repositories.Abstractions;

namespace RepositoryStore.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Update(product);
        await  context.SaveChangesAsync(cancellationToken);
        
        return product;
    }
    
    public async Task<Product> DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Remove(product);
        await  context.SaveChangesAsync(cancellationToken);
        
        return product;
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
    
    public async Task<List<Product>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context
            .Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}