using MyProject.Application.Interfaces;
using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _product = new();
    private static int _nextId = 1;

    // GET - get all products
    public Task<IEnumerable<Product>> GetAllAsync()
        => Task.FromResult<IEnumerable<Product>>(_product);

    // GET - get product by id
    public Task<Product?> GetByIdAsync(int id)
        => Task.FromResult(_product.FirstOrDefault(p => p.Id == id));

    // POST - create product
    public Task<Product> CreateAsync(Product product)
    {
        product.Id = _nextId++;
        _product.Add(product);
        return Task.FromResult(product);
    }

    // PUT - update product by id
    public Task<Product?> UpdateAsync(int id, Product updated)
    {
        var existing = _product.FirstOrDefault(p => p.Id == id);
        if (existing is null) return Task.FromResult<Product?>(null);

        existing.Name = updated.Name;
        existing.Price = updated.Price;
        return Task.FromResult<Product?>(existing);
    }

    // DELETE - delete product by id
    public Task<bool> DeleteAsync(int id)
    {
        var product = _product.FirstOrDefault(p => p.Id == id);
        if (product is null) return Task.FromResult(false);

        _product.Remove(product);
        return Task.FromResult(true);
    }
}
