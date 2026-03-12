import { useEffect, useState } from "react";
import type { Product, CreateProductRequest } from "../types/product";
import { getProducts, createProduct, deleteProduct } from "../api/productsApi";

export function useProducts() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    loadProducts();
  }, []);

  async function loadProducts() {
    try {
      setLoading(true);
      const data = await getProducts();
      setProducts(data);
    } catch {
      setError("Failed to load products");
    } finally {
      setLoading(false);
    }
  }

  async function handleCreate(data: CreateProductRequest) {
    try {
      const created = await createProduct(data);
      setProducts((prev) => [...prev, created]);
    } catch {
      setError("Failed to create product");
    }
  }

  async function handleDelete(id: number) {
    try {
      await deleteProduct(id);
      setProducts((prev) => prev.filter((p) => p.id !== id));
    } catch {
      setError("Failed to remove product");
    }
  }

  return { products, loading, error, handleCreate, handleDelete };
}
