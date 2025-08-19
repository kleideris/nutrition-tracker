import { useContext, useState } from "react";
import { type FoodItemDto } from "@/dto/FoodItemDto";
import { toast } from "sonner";
import { AuthContext } from "@/context/AuthContext";
import { fetchWithAuth } from "@/api/fetchWithAuth";

interface SelectedItem extends FoodItemDto {
  quantity: number;
  unit: string;
}

const measurementUnits = ["g", "serving"];
const mealTypes = ["Breakfast", "Lunch", "Dinner", "Snack"];

export const LogMealForm = () => {
  const [query, setQuery] = useState("");
  const [results, setResults] = useState<FoodItemDto[]>([]);
  const [selectedItems, setSelectedItems] = useState<SelectedItem[]>([]);
  const [mealType, setMealType] = useState("Lunch");

  const apiUrl = import.meta.env.VITE_API_URL;
  const auth = useContext(AuthContext);

  if (!auth || auth.loading) return null;
  const { user } = auth;

  const handleSearch = async () => {
    if (!apiUrl) {
      toast.error("API URL is not configured.");
      return;
    }

    try {
      const res = await fetchWithAuth(
        `/food-items/search?query=${encodeURIComponent(query.trim())}`,
        {
          method: "GET",
          headers: {
            "Cache-Control": "no-cache",
            Accept: "application/json",
          },
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
    }
  };

  const handleSelect = (item: FoodItemDto) => {
    const alreadyAdded = selectedItems.find((i) => i.name === item.name);
    if (alreadyAdded) return;

    setSelectedItems((prev) => [...prev, { ...item, quantity: 1, unit: "g" }]);
    setQuery("");
    setResults([]);
  };

  const updateItem = <K extends keyof SelectedItem>(
    index: number,
    field: K,
    value: SelectedItem[K]
  ) => {
    const updated = [...selectedItems];
    updated[index] = {
      ...updated[index],
      [field]: value,
    };
    setSelectedItems(updated);
  };

  const handleRemove = (index: number) => {
    const updated = [...selectedItems];
    updated.splice(index, 1);
    setSelectedItems(updated);
  };

  const handleLogMeal = async () => {
    if (!apiUrl) {
      toast.error("API URL is not configured.");
      return;
    }

    if (!user?.id) {
      toast.error("User not authenticated.");
      return;
    }

    const payload = {
      userId: parseInt(user.id), // Convert string to number if needed
      timestamp: new Date().toISOString(),
      mealFoodItems: selectedItems.map((item) => ({
        foodItemId: item.id,
        quantity: item.quantity,
        unitOfMeasurement: item.unit || "grams",
      })),
    };

    try {
      const res = await fetchWithAuth(`/meals/meal-type/${mealType}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
        body: JSON.stringify(payload),
      });

      const contentType = res.headers.get("content-type");
      if (!res.ok || !contentType?.includes("application/json")) {
        const raw = await res.text();
        console.error("Unexpected response:", raw);
        throw new Error("Invalid response format");
      }

      const data = await res.json();
      toast.success("Meal logged successfully!");
      console.log("Logged meal:", data);
      setSelectedItems([]);
    } catch (err) {
      console.error("Log meal failed:", err);
      toast.error("Failed to log meal. Please try again.");
    }
  };

  return (
    <div className="space-y-6">
      {/* Search Bar */}
      <div className="flex gap-4">
        <input
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder="Search food items..."
          className="flex-1 border rounded-md p-2 focus:outline-none focus:ring-2 bg-white focus:ring-green-400"
        />
        <button
          onClick={handleSearch}
          className="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600"
        >
          Search
        </button>
      </div>

      {/* Search Results */}
      <div className="space-y-2">
        {results.map((item) => (
          <div
            key={item.name}
            onClick={() => handleSelect(item)}
            className="cursor-pointer flex justify-between items-center bg-white p-4 rounded-md shadow-sm border hover:bg-gray-50"
          >
            <div className="font-semibold text-green-700">{item.name}</div>
            <div className="text-sm text-gray-600 flex gap-4">
              <span>🍽 {item.nutritionData.servingSizeGrams}g</span>
              <span>🔥 {item.nutritionData.calories} kcal</span>
              <span>🥩 {item.nutritionData.protein}g</span>
              <span>🍞 {item.nutritionData.carbohydrates}g</span>
              <span>🧈 {item.nutritionData.fats}g</span>
            </div>
          </div>
        ))}
      </div>

      {/* Selected Items */}
      {selectedItems.length > 0 && (
        <div className="space-y-4">
          <h3 className="font-semibold text-lg">Meal Details</h3>

          {/* Meal Type Selector */}
          <div className="flex items-center gap-4">
            <label className="font-medium">Meal Type:</label>
            <select
              value={mealType}
              onChange={(e) => setMealType(e.target.value)}
              className="border rounded px-2 py-1"
            >
              {mealTypes.map((type) => (
                <option key={type} value={type}>
                  {type}
                </option>
              ))}
            </select>
          </div>

          {/* Selected Food Items */}
          {selectedItems.map((item, index) => (
            <div
              key={item.name}
              className="flex items-center gap-4 bg-gray-50 p-4 rounded-md border"
            >
              <div className="flex-1">
                <div className="font-medium">{item.name}</div>
                <div className="text-xs text-gray-500">
                  {item.nutritionData.calories} kcal •{" "}
                  {item.nutritionData.protein}g protein •{" "}
                  {item.nutritionData.carbohydrates}g carbs •{" "}
                  {item.nutritionData.fats}g fat
                </div>
              </div>

              <input
                type="number"
                min={0}
                value={item.quantity}
                onChange={(e) =>
                  updateItem(index, "quantity", Number(e.target.value))
                }
                className="w-20 border rounded px-2 py-1"
              />

              <select
                value={item.unit}
                onChange={(e) => updateItem(index, "unit", e.target.value)}
                className="border rounded px-2 py-1"
              >
                {measurementUnits.map((unit) => (
                  <option key={unit} value={unit}>
                    {unit}
                  </option>
                ))}
              </select>

              <button
                onClick={() => handleRemove(index)}
                className="text-red-500 text-sm hover:underline"
              >
                Remove
              </button>
            </div>
          ))}

          <button
            onClick={handleLogMeal}
            className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
          >
            Log Meal
          </button>
        </div>
      )}
    </div>
  );
};