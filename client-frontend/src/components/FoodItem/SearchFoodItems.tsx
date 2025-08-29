import React, { useEffect, useState } from "react";
import { toast } from "sonner";
import { fetchWithAuth } from "@/api/fetchWithAuth";
import { Icons } from "../icons";
import type { FoodItemDto } from "@/types/foodItemDto";

export const SearchFoodItems: React.FC = () => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<FoodItemDto[]>([]);
  const [hasSearched, setHasSearched] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleSearch = async (searchQuery: string) => {
    const apiUrl = import.meta.env.VITE_API_URL;
    if (!apiUrl) {
      toast.error("API URL is not configured.");
      return;
    }

    try {
      setLoading(true);
      const res = await fetchWithAuth(
        `/food-items/search?query=${encodeURIComponent(searchQuery.trim())}`,
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
      setHasSearched(true);
    } catch (err) {
      console.error("Search failed:", err);
      toast.error("Search failed. Please try again.");
      setHasSearched(true);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const delayDebounce = setTimeout(() => {
      handleSearch(query);
    }, 300);

    return () => clearTimeout(delayDebounce);
  }, [query]);

  const handleFocus = () => {
    if (!query.trim()) {
      handleSearch("");
    }
  };

  return (
    <div className="space-y-4">
      <div className="flex gap-4">
        <input
          value={query}
          onChange={e => setQuery(e.target.value)}
          onFocus={handleFocus}
          placeholder="Search food items..."
          className="flex-1 border rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-green-400 bg-white/80"
        />
      </div>

      <div className="space-y-2">
        {loading ? (
          <div className="text-center animate-pulse italic">
            Searching…
          </div>
        ) : (
          <>
            {hasSearched && results.length === 0 && (
              <div className="text-center italic">No food items found.</div>
            )}

            {results.map(item => (
              <div
                key={item.name}
                className="flex justify-between items-center bg-white p-4 rounded-md shadow-sm border"
              >
                <div className="font-semibold text-green-700">{item.name}</div>
                <div className="text-sm text-gray-600 flex gap-4 items-center flex-wrap">
                  <span className="flex items-center gap-1">
                    <Icons.Serving />
                    serving's grams {item.nutritionData.servingSizeGrams}g
                  </span>
                  <span className="flex items-center gap-1">
                    <Icons.Calories />
                    calories {item.nutritionData.calories} kcal
                  </span>
                  <span className="flex items-center gap-1">
                    <Icons.Protein />
                    protein {item.nutritionData.protein}g
                  </span>
                  <span className="flex items-center gap-1">
                    <Icons.Carbs />
                    carbs {item.nutritionData.carbohydrates}g
                  </span>
                  <span className="flex items-center gap-1">
                    <Icons.Fats />
                    fats {item.nutritionData.fats}g
                  </span>
                </div>
              </div>
            ))}
          </>
        )}
      </div>
    </div>
  );
};