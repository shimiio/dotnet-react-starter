import { describe, it, expect, vi, beforeEach } from "vitest";
import { getProducts, createProduct, deleteProduct } from "./productsApi";

// describe — groups tests by meaning
describe("productsApi", () => {
  // beforeEach - executed before each test
  beforeEach(() => {
    // beforeEach - executed before each test
    vi.resetAllMocks();
  });

  describe("getProducts", () => {
    it("should return an array of products", async () => {
      // Arrange — mock fetch
      const fakeProducts = [
        { id: 1, name: "Apple", price: 1.5 },
        { id: 2, name: "Banana", price: 0.8 },
      ];

      global.fetch = vi.fn().mockResolvedValue({
        ok: true,
        json: async () => fakeProducts,
      });

      // Act
      const result = await getProducts();

      // Assert
      expect(result).toEqual(fakeProducts);
      expect(fetch).toHaveBeenCalledWith("http://localhost:5158/api/products");
    });

    it("throw an error if response.ok = false", async () => {
      // Arrange
      global.fetch = vi.fn().mockResolvedValue({
        ok: false,
        json: async () => null,
      });

      // Assert - check that the function throws an error
      await expect(getProducts()).rejects.toThrow("Error receiving products");
    });
  });

  describe("createProduct", () => {
    it("must send a POST request and return the created product", async () => {
      // Arrange
      const newProduct = { name: "Cherry", price: 2.0 };
      const createdProduct = { id: 3, name: "Cherry", price: 2.0 };

      global.fetch = vi.fn().mockResolvedValue({
        ok: true,
        json: async () => createdProduct,
      });

      // Act
      const result = await createProduct(newProduct);

      // Assert
      expect(result).toEqual(createdProduct);
      expect(fetch).toHaveBeenCalledWith(
        "http://localhost:5158/api/products",
        expect.objectContaining({
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(newProduct),
        }),
      );
    });
  });

  describe("deleteProduct", () => {
    it("must send a DELETE request", async () => {
      // Arrange
      global.fetch = vi.fn().mockResolvedValue({
        ok: true,
        json: async () => null,
      });

      // Act
      await deleteProduct(1);

      // Assert
      expect(fetch).toHaveBeenCalledWith(
        "http://localhost:5158/api/products/1",
        expect.objectContaining({ method: "DELETE" }),
      );
    });

    it("should throw an error if response.ok = false", async () => {
      global.fetch = vi.fn().mockResolvedValue({
        ok: false,
      });

      await expect(deleteProduct(1)).rejects.toThrow("Error deleting product");
    });
  });
});
