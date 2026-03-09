using MyProject.Application.Interfaces;
using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _product = new();
    private static int _nextId = 1;

    // GET - get all products
    public IEnumerable<Product> GetAll() => _product;

    // GET - get product by id
    public Product? GetById(int id) =>
        _product.FirstOrDefault(p => p.Id == id);

    // POST - create product
    public Product Create(Product product)
    {
        product.Id = _nextId++;
        _product.Add(product);
        return product;
    }

    // PUT - update product by id
    public Product? Update(int id, Product updated)
    {
        var existing = _product.FirstOrDefault(p => p.Id == id);
        if (existing is null) return null;

        existing.Name = updated.Name;
        existing.Price = updated.Price;
        return existing;
    }

    // DELETE - delete product by id
    public bool Delete(int id)
    {
        var product = _product.FirstOrDefault(p => p.Id == id);
        if (product is null) return false;

        _product.Remove(product);
        return true;
    }
}
