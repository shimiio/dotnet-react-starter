using MyProject.Application.Interfaces;
using MyProject.Infrastructure.Repositories;
using MyProject.Application.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();
app.MapControllers();
app.Run();
