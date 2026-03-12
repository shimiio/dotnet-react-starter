import type { Product, CreateProductRequest } from "../types/product";

const BASE_URL = "http://localhost:5158/api/products";

// GET - get all products
export async function getProducts(): Promise<Product[]> {
  const response = await fetch(BASE_URL);

  if (!response.ok) throw new Error("Error receiving products");

  return response.json();
}

// POST - create product
export async function createProduct(
  data: CreateProductRequest,
): Promise<Product> {
  const response = await fetch(BASE_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Error creating product");

  return response.json();
}

// DELETE - delete product by id
export async function deleteProduct(id: number): Promise<void> {
  const response = await fetch(`${BASE_URL}/${id}`, {
    method: "Delete",
  });

  if (!response.ok) throw new Error("Error deleting product");
}
