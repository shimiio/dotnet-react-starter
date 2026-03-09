using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    // GET /api/products - get all products
    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    // GET /api/products/1 - get product by id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _service.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    // POST /api/product/ - create product
    [HttpPost]
    public IActionResult Create([FromBody] CreateProductDto dto)
    {
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/product/1 - update product by id
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateProductDto dto)
    {
        var result = _service.Update(id, dto);
        return result is null ? NotFound() : Ok(result);
    }

    // DELETE /api/product/1 - delete product by id
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _service.Delete(id) ? NoContent() : NotFound();
    }
}
