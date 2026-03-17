using MyProject.Application.DTOs;

namespace MyProject.Application.Services;

public interface IProductService
{
    // GET - get all products
    Task<IEnumerable<ProductDto>> GetAllAsync();

    // GET - get product by id 
    Task<ProductDto?> GetByIdAsync(int id);

    // POST - create product
    Task<ProductDto> CreateAsync(CreateProductDto dto);

    // PUT - update product by id
    Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto);

    // DELETE - delete product by id
    Task<bool> DeleteAsync(int id);
}
