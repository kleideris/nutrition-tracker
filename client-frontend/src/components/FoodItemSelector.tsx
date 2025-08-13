import { useState, useEffect } from "react";
import { fetchWithAuth } from "@/api/fetchWithAuth";
import { toast } from "sonner";

interface FoodItem {
  id: number;
  name: string;
  calories: number;
  protein: number;
  carbs: number;
  fat: number;
}

export default function FoodItemSelector({ onSelect }: { onSelect: (item: FoodItem) => void }) {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<FoodItem[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (query.length < 2) return;

    const fetchResults = async () => {
      setLoading(true);
      try {
        const res = await fetchWithAuth(`/fooditems/search?query=${query}`);
        if (!res.ok) throw new Error("Search failed");
        const data = await res.json();
        setResults(data);
      } catch {
        toast.error("Failed to fetch food items");
      } finally {
        setLoading(false);
      }
    };

    const debounce = setTimeout(fetchResults, 300);
    return () => clearTimeout(debounce);
  }, [query]);

  return (
    <div className="space-y-2">
      <input
        type="text"
        value={query}
        onChange={(e) => setQuery(e.target.value)}
        placeholder="Search food..."
        className="w-full border border-gray-300 rounded px-3 py-2"
      />

      {loading && <p className="text-sm text-gray-500">Searching...</p>}

      <ul className="space-y-2">
        {results.map((item) => (
          <li
            key={item.id}
            onClick={() => onSelect(item)}
            className="p-2 border rounded cursor-pointer hover:bg-gray-100"
          >
            <div className="font-medium">{item.name}</div>
            <div className="text-xs text-gray-500">
              {item.calories} kcal • {item.protein}g protein • {item.carbs}g carbs • {item.fat}g fat
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}