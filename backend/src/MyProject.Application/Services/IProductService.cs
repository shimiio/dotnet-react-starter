using MyProject.Application.DTOs;

namespace MyProject.Application.Services;

public interface IProductService
{
    // GET - get all products
    IEnumerable<ProductDto> GetAll();

    // GET - get product by id 
    ProductDto? GetById(int id);

    // POST - create product
    ProductDto Create(CreateProductDto Dto);

    // PUT - update product by id
    ProductDto? Update(int id, UpdateProductDto dto);

    // DELETE - delete product by id
    bool Delete(int id);
}
