import { describe, it, expect, vi, beforeEach } from "vitest";
import { renderHook, act, waitFor } from "@testing-library/react";
import { useProducts } from "./useProducts";
import * as api from "../api/productsApi";

vi.mock("../api/productsApi");

describe("useProducts", () => {
  const fakeProducts = [
    { id: 1, name: "Apple", price: 1.5 },
    { id: 2, name: "Banana", price: 0.8 },
  ];

  beforeEach(() => {
    vi.resetAllMocks();
  });

  it("must load products during initialization", async () => {
    // Arrange
    vi.mocked(api.getProducts).mockResolvedValue(fakeProducts);

    // Act
    const { result } = renderHook(() => useProducts());

    // loading = true
    expect(result.current.loading).toBe(true);

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    // Assert
    expect(result.current.products).toEqual(fakeProducts);
    expect(result.current.error).toBeNull();
  });

  it("should set error if download fails", async () => {
    // Arrange
    vi.mocked(api.getProducts).mockRejectedValue(
      new Error("Сервер недоступен"),
    );

    // Act
    const { result } = renderHook(() => useProducts());

    await waitFor(() => {
      expect(result.current.loading).toBe(false);
    });

    // Assert
    expect(result.current.error).toBe("Failed to load products");
    expect(result.current.products).toEqual([]);
  });

  it("handleCreate should add the product to the list", async () => {
    // Arrange
    vi.mocked(api.getProducts).mockResolvedValue(fakeProducts);
    const newProduct = { id: 3, name: "Cherry", price: 2.0 };
    vi.mocked(api.createProduct).mockResolvedValue(newProduct);

    const { result } = renderHook(() => useProducts());

    await waitFor(() => expect(result.current.loading).toBe(false));

    // Act
    await act(async () => {
      await result.current.handleCreate({
        name: "Cherry",
        price: 2.0,
      });
    });

    // Assert
    expect(result.current.products).toHaveLength(3);
    expect(result.current.products).toContainEqual(newProduct);
  });

  it("handleDelete should remove the product from the list", async () => {
    // Arrange
    vi.mocked(api.getProducts).mockResolvedValue(fakeProducts);
    vi.mocked(api.deleteProduct).mockResolvedValue(undefined);

    const { result } = renderHook(() => useProducts());

    await waitFor(() => expect(result.current.loading).toBe(false));

    // Act
    await act(async () => {
      await result.current.handleDelete(1);
    });

    // Assert
    expect(result.current.products).toHaveLength(1);
    expect(result.current.products).not.toContainEqual(
      expect.objectContaining({ id: 1 }),
    );
  });
});
