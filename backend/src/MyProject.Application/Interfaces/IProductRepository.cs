using MyProject.Domain.Models;

namespace MyProject.Application.Interfaces;

public interface IProductRepository
{
    // GET - get all products
    Task<IEnumerable<Product>> GetAllAsync();

    // GET - get product by id 
    Task<Product?> GetByIdAsync(int id);

    // POST - create product
    Task<Product> CreateAsync(Product product);

    // PUT - update product by id
    Task<Product?> UpdateAsync(int id, Product updated);

    // DELETE - delete product by id
    Task<bool> DeleteAsync(int id);
}
