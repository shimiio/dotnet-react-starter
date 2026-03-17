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
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        return products.Select(ToDto);
    }

    // GET - get product by id
    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product is null ? null : ToDto(product);
    }

    // POST - create product
    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        var created = await _repo.CreateAsync(product);

        return ToDto(created);
    }

    // PUT - update product by id
    public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        var updated = await _repo.UpdateAsync(id, product);
        return updated is null ? null : ToDto(updated);
    }

    // DELETE - delete product by id
    public async Task<bool> DeleteAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }

    private static ProductDto ToDto(Product product) => new ProductDto
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price
    };
}
