import { useContext, useEffect, useState } from "react";
import { toast } from "sonner";
import { AuthContext } from "@/context/AuthContext";
import { fetchWithAuth } from "@/api/fetchWithAuth";
import { type FoodItemDto } from "@/types/foodItemDto";
import { type MealFormProps, type SelectedItem } from "@/types/meals";

import { SearchBar } from "@/components/Meal/SearchBar";
import { SearchResults } from "@/components/Meal/SearchResults";
import { MealMeta } from "@/components/Meal/MealMeta";
import { SelectedItemsList } from "@/components/Meal/SelectedItemsList";

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
    setSelectedItems((prev) => [...prev, { ...item, quantity: 100, unit: "g" }]);
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

    const endpoint = mode === "create"
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
      <SearchBar query={query} setQuery={(val) => { setQuery(val); setHasInteracted(true); }} handleFocus={handleFocus} />
      <SearchResults results={results} loading={loading} query={query} handleSelect={handleSelect} />
      <MealMeta
        mealType={mealType}
        setMealType={setMealType}
        timestamp={timestamp}
        setTimestamp={setTimestamp}
        handleSubmit={handleSubmit}
        mode={mode}
      />
      <SelectedItemsList
        selectedItems={selectedItems}
        updateItem={updateItem}
        handleRemove={handleRemove}
      />
    </div>
  );
};