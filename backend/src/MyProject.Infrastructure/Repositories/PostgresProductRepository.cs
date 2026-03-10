using Microsoft.EntityFrameworkCore;
using MyProject.Application.Interfaces;
using MyProject.Domain.Models;
using MyProject.Infrastructure.Data;

namespace MyProject.Infrastructure.Repositories;

public class PostgresProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public PostgresProductRepository(AppDbContext db)
    {
        _db = db;
    }

    // GET - get all products
    public IEnumerable<Product> GetAll()
    {
        return _db.Products.AsNoTracking().ToList();
    }

    // GET - get product by id
    public Product? GetById(int id)
    {
        return _db.Products.Find(id);
    }

    // POST - create product
    public Product Create(Product product)
    {
        _db.Products.Add(product);
        _db.SaveChanges();

        return product;
    }

    // PUT - update product by id
    public Product? Update(int id, Product updated)
    {
        var existing = _db.Products.Find(id);
        if (existing is null) return null;

        existing.Name = updated.Name;
        existing.Price = updated.Price;

        _db.SaveChanges();
        return existing;
    }

    // DELETE - delete product by id
    public bool Delete(int id)
    {
        var existing = _db.Products.Find(id);
        if (existing is null) return false;

        _db.Products.Remove(existing);
        _db.SaveChanges();
        return true;
    }
}
