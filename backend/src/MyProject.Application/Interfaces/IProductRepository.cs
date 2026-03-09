using MyProject.Domain.Models;

namespace MyProject.Application.Interfaces;

public interface IProductRepository
{
    // GET - get all products
    IEnumerable<Product> GetAll();

    // GET - get product by id 
    Product? GetById(int id);

    // POST - create product
    Product Create(Product product);

    // PUT - update product by id
    Product? Update(int id, Product updated);

    // DELETE - delete product by id
    bool Delete(int id);
}
