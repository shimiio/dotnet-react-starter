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
    public void GetAll_ShouldReturnAllProducts()
    {
        // Arrange
        var fakeProducts = new List<Product>
        {
            new Product { Id = 1, Name = "Apple", Price = 1.5m },
            new Product { Id = 2, Name = "Banana", Price = 0.8m }
        };
        _mockRepo.Setup(r => r.GetAll()).Returns(fakeProducts);

        // Act
        var result = _service.GetAll();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(p => p.Name == "Apple");
    }

    [Fact]
    public void GetById_ShouldReturnProduct_WhenExist()
    {
        // Arrange
        var fakeProduct = new Product { Id = 1, Name = "Apple", Price = 1.5m };
        _mockRepo.Setup(r => r.GetById(1)).Returns(fakeProduct);

        // Act
        var result = _service.GetById(1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Name.Should().Be("Apple");
    }

    [Fact]
    public void GetById_ShouldReturnProduct_WhenNotExists()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetById(99)).Returns((Product?)null);

        // Act
        var result = _service.GetById(99);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Create_ShouldReturnCreatedPeroduct()
    {
        // Arrange
        var dto = new CreateProductDto { Name = "Apple", Price = 1.5m };

        _mockRepo.Setup(r => r.Create(It.IsAny<Product>()))
            .Returns((Product p) =>
            {
                p.Id = 1;
                return p;
            });

        // Act
        var result = _service.Create(dto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Apple");
        result.Price.Should().Be(1.5m);

        _mockRepo.Verify(r => r.Create(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public void Product_ShouldReturnTrue_WhenProductExists()
    {
        // Arrange
        _mockRepo.Setup(r => r.Delete(1)).Returns(true);

        // Act
        var result = _service.Delete(1);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Product_ShouldReturnFalse_WhenProductExists()
    {
        // Arrange
        _mockRepo.Setup(r => r.Delete(99)).Returns(false);

        // Act
        var result = _service.Delete(99);

        // Assert
        result.Should().BeFalse();
    }
}
