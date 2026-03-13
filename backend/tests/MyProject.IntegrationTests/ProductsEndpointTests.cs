using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using MyProject.Application.DTOs;
using MyProject.Infrastructure.Data;

namespace MyProject.IntegrationTests;

public class ProductsEndpointTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres;
    private HttpClient _client = null!;
    private WebApplicationFactory<Program> _factory = null!;

    public ProductsEndpointTests()
    {
        _postgres = new PostgreSqlBuilder("postgres:latest")
            .WithDatabase("testdb")
            .WithDatabase("postgres")
            .WithPassword("postgres")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(host =>
            {
                host.UseEnvironment("Testing");

                host.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(_postgres.GetConnectionString()));
                });
            });
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();

        _client = _factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
        await _postgres.DisposeAsync();
    }

    [Fact]
    public async Task CreateProduct_ShouldReturn201_WithCreatedProduct()
    {
        // Arrange
        var request = new CreateProductDto { Name = "Apple", Price = 1.5m };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var product = await response.Content.ReadFromJsonAsync<ProductDto>();
        product.Should().NotBeNull();
        product!.Id.Should().BeGreaterThan(0);
        product.Name.Should().Be("Apple");
        product.Price.Should().Be(1.5m);
    }

    [Fact]
    public async Task GetProducts_ShouldReturn200_WithList()
    {
        // Arrange
        await _client.PostAsJsonAsync("/api/products",
            new CreateProductDto { Name = "Banana", Price = 0.8m });

        // Act
        var response = await _client.GetAsync("/api/products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        products.Should().NotBeNull();
        products!.Should().Contain(p => p.Name == "Banana");
    }

    [Fact]
    public async Task DeleteProduct_ShouldReturn204_WhenExists()
    {
        // Arrange
        var createResponse = await _client.PostAsJsonAsync("/api/products",
            new CreateProductDto { Name = "Cherry", Price = 2.0m });
        var created = await createResponse.Content.ReadFromJsonAsync<ProductDto>();

        // Act
        var response = await _client.DeleteAsync($"/api/products/{created!.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetProduct_ShouldReturn404_WhenNonExists()
    {
        // Act
        var response = await _client.GetAsync("/api/product/99999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
