using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;

var builder = WebApplication.CreateBuilder(args);
var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(s =>
{
    s.UseSqlServer(conectionString);

});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
