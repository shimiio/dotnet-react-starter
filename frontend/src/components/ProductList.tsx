import type { Product } from "../types/product";

interface Props {
  products: Product[];
  onDelete: (id: number) => void;
}

export default function ProductList({ products, onDelete }: Props) {
  if (products.length === 0) {
    return <p className="text-white">There are no products yet</p>;
  }

  return (
    <ul className="space-y-2">
      {products.map((product) => (
        <li
          key={product.id}
          className="flex items-center justify-between text-white p-3 border border-white rounded"
        >
          <span>
            {product.name} - ${product.price}
          </span>
          <button
            onClick={() => onDelete(product.id)}
            className="cursor-pointer px-3 py-1 text-white bg-red-500 rounded hover:bg-red-600"
          >
            Delete
          </button>
        </li>
      ))}
    </ul>
  );
}
