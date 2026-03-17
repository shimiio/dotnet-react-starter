using FluentAssertions;
using Moq;
using MyProject.Application.Interfaces;
using MyProject.Application.Services;
using MyProject.Domain.Models;

namespace MyProject.UnitTests.Services;

public class ProductServiceTest
{
    private readonly Mock<IProductRepository> _mockRepo;
    private readonly ProductService _service;

    public ProductServiceTest()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllProducts()
    {
        // Arrange
        var fakeProducts = new List<Product>
        {
            new Product { Id = 1, Name = "Apple", Price = 1.5m },
            new Product { Id = 2, Name = "Banana", Price = 0.8m }
        };
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeProducts);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(p => p.Name == "Apple");
    }

    [Fact]
    public async Task GetById_ShouldReturnProduct_WhenExist()
    {
        // Arrange
        var fakeProduct = new Product { Id = 1, Name = "Apple", Price = 1.5m };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fakeProduct);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Name.Should().Be("Apple");
    }

    [Fact]
    public async Task GetById_ShouldReturnProduct_WhenNotExists()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Product?)null);

        // Act
        var result = await _service.GetByIdAsync(99);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedPeroduct()
    {
        // Arrange
        var dto = new CreateProductDto { Name = "Apple", Price = 1.5m };

        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Product>()))
            .ReturnsAsync((Product p) =>
            {
                p.Id = 1;
                return p;
            });

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Apple");
        result.Price.Should().Be(1.5m);

        _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task Product_ShouldReturnTrue_WhenProductExistsAsync()
    {
        // Arrange
        _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(1);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Product_ShouldReturnFalse_WhenProductExists()
    {
        // Arrange
        _mockRepo.Setup(r => r.DeleteAsync(99)).ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(99);

        // Assert
        result.Should().BeFalse();
    }
}
