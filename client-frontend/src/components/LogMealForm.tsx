import { useContext, useEffect, useState } from "react";
import { type FoodItemDto } from "@/dto/FoodItemDto";
import { toast } from "sonner";
import { AuthContext } from "@/context/AuthContext";
import { fetchWithAuth } from "@/api/fetchWithAuth";

interface SelectedItem extends FoodItemDto {
  quantity: number;
  unit: string;
}

interface MealFormProps {
  mode: "create" | "edit";
  initialMeal?: {
    id: number;
    mealType: string;
    timestamp: string;
    foodItems: {
      foodItemId: number;
      quantity: number;
      unitOfMeasurement: string;
      foodName?: string;
    }[];
  };
  onSuccess?: () => void;
}

const measurementUnits = ["g", "serving"];
const mealTypes = ["Breakfast", "Lunch", "Dinner", "Snack"];

export const LogMealForm = ({ mode, initialMeal, onSuccess }: MealFormProps) => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<FoodItemDto[]>([]);
  const [selectedItems, setSelectedItems] = useState<SelectedItem[]>([]);
  const [mealType, setMealType] = useState("Lunch");
  const [timestamp, setTimestamp] = useState(new Date().toISOString());
  const [loading, setLoading] = useState(false);
  const [hasInteracted, setHasInteracted] = useState(false);

  const auth = useContext(AuthContext);
  const { user } = auth ?? {};

  useEffect(() => {
    if (mode === "edit" && initialMeal) {
      setMealType(initialMeal.mealType);
      setTimestamp(initialMeal.timestamp);
      setSelectedItems(
        initialMeal.foodItems.map((item) => ({
          id: item.foodItemId,
          name: item.foodName ?? `Food #${item.foodItemId}`,
          quantity: item.quantity,
          unit: item.unitOfMeasurement,
          nutritionData: {
            calories: 0,
            protein: 0,
            carbohydrates: 0,
            fats: 0,
            servingSizeGrams: 0,
          },
        }))
      );
    }
  }, [mode, initialMeal]);

  const handleSearch = async (searchQuery: string) => {
    try {
      setLoading(true);
      const res = await fetchWithAuth(`/food-items/search?query=${encodeURIComponent(searchQuery.trim())}`);
      const data = await res.json();
      setResults(data);
    } catch {
      toast.error("Search failed.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (!hasInteracted) return;

    const delayDebounce = setTimeout(() => {
      handleSearch(query);
    }, 300);

    return () => clearTimeout(delayDebounce);
  }, [query, hasInteracted]);

  const handleFocus = () => {
    if (!query.trim()) {
      setHasInteracted(true);
      handleSearch("");
    }
  };

  const handleSelect = (item: FoodItemDto) => {
    if (selectedItems.find((i) => i.id === item.id)) return;
    setSelectedItems((prev) => [...prev, { ...item, quantity: 1, unit: "g" }]);
    setQuery("");
    setResults([]);
  };

  const updateItem = <K extends keyof SelectedItem>(index: number, field: K, value: SelectedItem[K]) => {
    const updated = [...selectedItems];
    updated[index] = { ...updated[index], [field]: value };
    setSelectedItems(updated);
  };

  const handleRemove = (index: number) => {
    const updated = [...selectedItems];
    updated.splice(index, 1);
    setSelectedItems(updated);
  };

  const handleSubmit = async () => {
    if (!user?.id) {
      toast.error("User not authenticated.");
      return;
    }

    const payload = {
      userId: parseInt(user.id),
      timestamp,
      mealType,
      mealFoodItems: selectedItems.map((item) => ({
        foodItemId: item.id,
        quantity: item.quantity,
        unitOfMeasurement: item.unit,
      })),
    };

    const endpoint =
      mode === "create"
        ? `/meals/meal-type/${mealType}`
        : `/meals/${initialMeal?.id}`;

    const method = mode === "create" ? "POST" : "PUT";

    try {
      const res = await fetchWithAuth(endpoint, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      if (!res.ok) throw new Error("Request failed");

      toast.success(mode === "create" ? "Meal logged!" : "Meal updated!");
      setSelectedItems([]);
      onSuccess?.();
    } catch {
      toast.error("Failed to submit meal.");
    }
  };

  return (
    <div className="space-y-6 pb-12">
      {/* Search Bar */}
      <div className="flex gap-4">
        <input
          value={query}
          onChange={(e) => {
            setQuery(e.target.value);
            setHasInteracted(true);
          }}
          onFocus={handleFocus}
          placeholder="Search food items..."
          className="flex-1 border rounded-md p-2 bg-white"
        />
      </div>

      {/* Search Results */}
      <div className="space-y-2">
        {loading ? (
          <div className="text-green-500 animate-pulse italic">Searching…</div>
        ) : (
          <>
            {results.length === 0 && query.trim() !== "" && (
              <div className="italic">No food items found.</div>
            )}
            {results.map((item) => (
              <div
                key={item.id}
                onClick={() => handleSelect(item)}
                className="cursor-pointer bg-white p-4 rounded-md border hover:bg-gray-50"
              >
                <div className="font-semibold text-green-700">{item.name}</div>
              </div>
            ))}
          </>
        )}
      </div>

      {/* Meal Type, Timestamp and Log Meal button */}
      <div className="flex justify-between items-center flex-wrap gap-4">
        <div className="flex gap-4 items-center flex-wrap">
          <label>Meal Type:</label>
          <select value={mealType} onChange={(e) => setMealType(e.target.value)} className="border rounded px-2 py-1">
            {mealTypes.map((type) => (
              <option key={type}>{type}</option>
            ))}
          </select>

          <label>Timestamp:</label>
          <input
            type="datetime-local"
            value={new Date(timestamp).toISOString().slice(0, 16)}
            onChange={(e) => setTimestamp(new Date(e.target.value).toISOString())}
            className="border rounded px-2 py-1"
          />
        </div>
        <button
          onClick={handleSubmit}
          className="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600"
        >
          {mode === "create" ? "Log Meal" : "Update Meal"}
        </button>
      </div>

      {/* Selected Items */}
      <div className="max-h-[300px] overflow-y-auto space-y-2 border rounded-md p-2 bg-gray-50">
        {selectedItems.map((item, index) => (
          <div key={item.id} className="flex items-center gap-4 bg-white p-4 rounded-md border">
            <div className="flex-1">
              <div className="font-medium">{item.name}</div>
            </div>
            <input
              type="number"
              value={item.quantity}
              onChange={(e) => updateItem(index, "quantity", Number(e.target.value))}
              className="w-20 border rounded px-2 py-1"
            />
            <select
              value={item.unit}
              onChange={(e) => updateItem(index, "unit", e.target.value)}
              className="border rounded px-2 py-1"
            >
              {measurementUnits.map((unit) => (
                <option key={unit}>{unit}</option>
              ))}
            </select>
            <button onClick={() => handleRemove(index)} className="text-red-500 text-sm hover:underline">
              Remove
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};