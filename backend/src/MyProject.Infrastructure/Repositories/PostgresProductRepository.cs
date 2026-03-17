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
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _db.Products.AsNoTracking().ToListAsync();
    }

    // GET - get product by id
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _db.Products.FindAsync(id);
    }

    // POST - create product
    public async Task<Product> CreateAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        return product;
    }

    // PUT - update product by id
    public async Task<Product?> UpdateAsync(int id, Product updated)
    {
        var existing = await _db.Products.FindAsync(id);
        if (existing is null) return null;

        existing.Name = updated.Name;
        existing.Price = updated.Price;

        await _db.SaveChangesAsync();
        return existing;
    }

    // DELETE - delete product by id
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Products.FindAsync(id);
        if (existing is null) return false;

        _db.Products.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
