using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;
using RepositoryStore.Models;
using RepositoryStore.Repositories;
using RepositoryStore.Repositories.Abstractions;

var builder = WebApplication.CreateBuilder(args);
var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(s => { s.UseSqlServer(conectionString); });

builder.Services.AddTransient<IProductRepository, ProductRepository>();

var app = builder.Build();

app.MapGet("/v1/products", async (CancellationToken token, IProductRepository repository)
    => Results.Ok(await repository.GetAllAsync()));

app.MapGet("/v1/products/{id}", async (int id, CancellationToken token, IProductRepository repository)
    => Results.Ok(await repository.GetByIdAsync(id)));

app.MapPost("/v1/products", async (CancellationToken token, IProductRepository repository, Product product)
    => Results.Ok(await repository.CreateAsync(product, token)));

app.MapPut("/v1/products", async (CancellationToken token, IProductRepository repository, Product product)
    => Results.Ok(await repository.UpdateAsync(product, token)));

app.MapDelete("/v1/products/{id}", async (CancellationToken token, IProductRepository repository, int id)
    =>
{
    var product = await repository.GetByIdAsync(id);
    return product == null ? Results.NotFound() : Results.Ok(await repository.DeleteAsync(product, token));
});

app.Run();