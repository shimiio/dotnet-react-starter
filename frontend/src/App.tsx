import { useProducts } from "./hooks/useProducts";
import ProductList from "./components/ProductList";
import CreateproductForm from "./components/CreateProductForm";

export default function App() {
  const { products, loading, error, handleCreate, handleDelete } =
    useProducts();

  return (
    <div className="min-h-screen bg-mist-950 p-15">
      <div className="max-w-2xl mx-auto">
        <h1 className="text-2xl text-white font-bold mb-6">Products</h1>

        <CreateproductForm onCreate={handleCreate} />

        {error && <p className="mb-4 text-red-500">{error}</p>}

        {loading ? (
          <p className="text-white">Loading...</p>
        ) : (
          <ProductList products={products} onDelete={handleDelete} />
        )}
      </div>
    </div>
  );
}
