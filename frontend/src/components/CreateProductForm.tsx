import { useState } from "react";
import type { CreateProductRequest } from "../types/product";

interface Props {
  onCreate: (data: CreateProductRequest) => void;
}

export default function CreateproductForm({ onCreate }: Props) {
  const [name, setName] = useState("");
  const [price, setPrice] = useState("");

  function handleSubmit() {
    if (!name.trim() || !price) return;

    onCreate({
      name: name.trim(),
      price: parseFloat(price),
    });

    setName("");
    setPrice("");
  }

  return (
    <div className="flex gap-2 mb-6">
      <input
        type="text"
        placeholder="Name"
        value={name}
        onChange={(e) => setName(e.target.value)}
        className="flex-1 text-white px-3 py-2 border border-white rounded"
      />
      <input
        type="number"
        placeholder="Price"
        value={price}
        onChange={(e) => setPrice(e.target.value)}
        className="text-white w-32 px-3 py-2 border border-white rounded"
      />
      <button
        onClick={handleSubmit}
        className="cursor-pointer px-4 py-2 text-white bg-blue-500 rounded hover:bg-blue-600"
      >
        Add
      </button>
    </div>
  );
}
