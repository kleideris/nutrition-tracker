import { useState } from "react";
import { fetchWithAuth } from "@/api/fetchWithAuth";
import { toast } from "sonner";
import BackButton from "@/components/BackButton";
import FoodItemSelector from "@/components/FoodItemSelector";

export default function LogMealForm() {
  const [mealType, setMealType] = useState("Breakfast");
  const [timestamp, setTimestamp] = useState(new Date().toISOString().slice(0, 10));
  const [selectedFoodItems, setSelectedFoodItems] = useState<any[]>([]);
  const [currentFood, setCurrentFood] = useState<any | null>(null);
  const [quantity, setQuantity] = useState<number>(1);
  const [unit, setUnit] = useState<string>("g");

  const handleFoodSelect = (item: any) => {
    setCurrentFood(item);
    toast.success(`Selected: ${item.name}`);
  };

  const handleAddFoodItem = () => {
    if (!currentFood) return toast.error("Select a food item first");
    if (quantity <= 0) return toast.error("Quantity must be greater than 0");

    const newItem = {
      foodItemId: currentFood.id,
      quantity,
      unitOfMeasurement: unit,
    };

    setSelectedFoodItems((prev) => [...prev, newItem]);
    setCurrentFood(null);
    setQuantity(1);
    setUnit("g");
    toast.success(`Added ${currentFood.name} to meal`);
  };

  const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();

  const dto = {
    userId: 1, // ideally from context
    timestamp: new Date(timestamp).toISOString(), // ensure ISO format
    mealFoodItems: selectedFoodItems.map(item => ({
      foodItemId: item.foodItemId,
      quantity: parseFloat(item.quantity),
      unitOfMeasurement: item.unitOfMeasurement
    }))
  };

  try {
    const res = await fetchWithAuth(`/meals/meal-type/${mealType}`, {
      method: "POST",
      body: JSON.stringify(dto),
    });

    if (!res.ok) {
      const error = await res.json();
      toast.error(`❌ ${error.message || "Failed to log meal."}`);
      return;
    }

    toast.success("✅ Meal logged successfully!");
    setSelectedFoodItems([]);
  } catch {
    toast.error("❌ Network error while logging meal.");
  }
};

  return (
    <div className="max-w-xl mx-auto bg-white p-8 rounded-lg shadow">
      <BackButton />
      <h2 className="text-2xl font-semibold mb-6 border-b pb-2">🍽️ Log a Meal</h2>
      <form onSubmit={handleSubmit} className="space-y-6">
        <div>
          <label className="block font-medium mb-1">Meal Type</label>
          <select value={mealType} onChange={(e) => setMealType(e.target.value)} className="w-full border rounded px-3 py-2">
            <option>Breakfast</option>
            <option>Lunch</option>
            <option>Dinner</option>
            <option>Snack</option>
          </select>
        </div>

        <div>
          <label className="block font-medium mb-1">Date</label>
          <input type="date" value={timestamp} onChange={(e) => setTimestamp(e.target.value)} className="w-full border rounded px-3 py-2" />
        </div>

        <div>
          <label className="block font-medium mb-1">Search Food Item</label>
          <FoodItemSelector onSelect={handleFoodSelect} />
        </div>

        {currentFood && (
          <div className="border p-4 rounded bg-gray-50 space-y-4">
            <div className="text-sm text-gray-700 font-medium">Selected: {currentFood.name}</div>

            <div className="flex gap-4">
              <div className="flex-1">
                <label className="block text-sm font-medium mb-1">Quantity</label>
                <input
                  type="number"
                  value={quantity}
                  onChange={(e) => setQuantity(parseFloat(e.target.value))}
                  className="w-full border rounded px-3 py-2"
                />
              </div>

              <div className="flex-1">
                <label className="block text-sm font-medium mb-1">Unit</label>
                <select value={unit} onChange={(e) => setUnit(e.target.value)} className="w-full border rounded px-3 py-2">
                  <option value="g">grams (g)</option>
                  <option value="ml">milliliters (ml)</option>
                  <option value="pcs">pieces (pcs)</option>
                </select>
              </div>
            </div>

            <button
              type="button"
              onClick={handleAddFoodItem}
              className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700 transition"
            >
              Add to Meal
            </button>
          </div>
        )}

        <ul className="mt-6 space-y-2">
          {selectedFoodItems.map((item, index) => (
            <li key={index} className="text-sm text-gray-700 border-b pb-2">
              Food ID: {item.foodItemId}, Quantity: {item.quantity} {item.unitOfMeasurement}
            </li>
          ))}
        </ul>

        <button type="submit" className="w-full bg-green-600 text-white py-2 rounded hover:bg-green-700 transition">
          Log Meal
        </button>
      </form>
    </div>
  );
}