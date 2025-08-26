import React, { useState } from "react";
import { type FoodItemDto } from "@/dto/FoodItemDto";
import { toast } from "sonner";
import { fetchWithAuth } from "@/api/fetchWithAuth";
import { Icons } from "../icons";

type Props = {
  apiUrl: string;
};

const RemoveFoodItem: React.FC<Props> = ({ apiUrl }) => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<FoodItemDto[]>([]);
  const [loading, setLoading] = useState(false);

  const handleSearch = async () => {
    if (!apiUrl) {
      toast.error("API URL is not configured.");
      return;
    }

    try {
      setLoading(true);
      const res = await fetchWithAuth(
        `/food-items/search?query=${encodeURIComponent(query.trim())}`,
        {
          method: "GET",
          headers: {
            "Cache-Control": "no-cache",
            "Accept": "application/json"
          }
        }
      );

      const contentType = res.headers.get("content-type");
      if (!res.ok || !contentType?.includes("application/json")) {
        const raw = await res.text();
        console.error("Unexpected response:", raw);
        throw new Error("Invalid response format");
      }

      const data = await res.json();
      setResults(data);
    } catch (err) {
      console.error("Search failed:", err);
      toast.error("Search failed. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  const handleRemove = async (id: number, name: string) => {
    try {
      const res = await fetchWithAuth(`/food-items/${id}`, {
        method: "DELETE"
      });

      if (!res.ok) throw new Error("Failed to delete food item");

      toast.success(`Removed ${name}`);
      handleSearch(); // Refresh results
    } catch (err) {
      console.error("Error removing food item:", err);
      toast.error("Failed to remove item.");
    }
  };

  return (
    <div className="space-y-4">
      <div className="flex gap-4">
        <input
          value={query}
          onChange={e => setQuery(e.target.value)}
          placeholder="Search food items..."
          className="flex-1 border rounded-md p-2 focus:outline-none focus:ring-2 bg-white/80 focus:ring-green-400"
        />
        <button
          onClick={handleSearch}
          className="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600"
        >
          Search
        </button>
      </div>

      {loading ? (
        <p>Loading...</p>
      ) : (
        <div className="space-y-2">
          {results.map(item => (
            <div
              key={item.name}
              className="flex justify-between items-center bg-white p-4 rounded-md shadow-sm border"
            >
              <div>
                <div className="font-semibold text-green-700">{item.name}</div>
                <div className="text-sm text-gray-600 flex gap-4 items-center flex-wrap">
                  <span className="flex items-center gap-1">
                    <Icons.Serving />
                    serving {item.nutritionData.servingSizeGrams}g
                  </span>
                  <span className="flex items-center gap-1">
                    <Icons.Calories />
                    {item.nutritionData.calories} kcal
                  </span>
                  <span className="flex items-center gap-1">
                    <Icons.Protein />
                    {item.nutritionData.protein}g protein
                  </span>
                  <span className="flex items-center gap-1">
                    <Icons.Carbs />
                    {item.nutritionData.carbohydrates}g carbs
                  </span>
                  <span className="flex items-center gap-1">
                    <Icons.Fats />
                    {item.nutritionData.fats}g fats
                  </span>
                </div>
              </div>
              <button
                onClick={() => {
                  if (item.id !== undefined) {
                    handleRemove(item.id, item.name);
                  } else {
                    toast.error("Item ID is missing. Cannot delete.");
                  }
                }}
                className="px-3 py-1 bg-red-600 text-white rounded-md hover:bg-red-700"
              >
                Remove
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default RemoveFoodItem;