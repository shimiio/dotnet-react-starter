using MyProject.Application.DTOs;
using MyProject.Application.Interfaces;
using MyProject.Domain.Models;

namespace MyProject.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    // GET - get all products
    public IEnumerable<ProductDto> GetAll()
    {
        return _repo.GetAll().Select(ToDto);
    }

    // GET - get product by id
    public ProductDto? GetById(int id)
    {
        var product = _repo.GetById(id);
        return product is null ? null : ToDto(product);
    }

    // POST - create product
    public ProductDto Create(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        var created = _repo.Create(product);

        return ToDto(created);
    }

    // PUT - update product by id
    public ProductDto? Update(int id, UpdateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        var updated = _repo.Update(id, product);
        return updated is null ? null : ToDto(updated);
    }

    // DELETE - delete product by id
    public bool Delete(int id) => _repo.Delete(id);

    private static ProductDto ToDto(Product product) => new ProductDto
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price
    };
}
