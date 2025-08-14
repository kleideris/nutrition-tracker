import { useState } from "react";
import { SearchFoodItems } from "../components/FoodItem/SearchFoodItems";
import AddFoodItem from "../components/FoodItem/AddFoodItem";
import BackButton from "@/components/BackButton";
import { toast } from "sonner";

type FoodItemType = {
  name: string;
  nutritionData: {
    calories: number;
    protein: number;
    carbohydrates: number;
    fats: number;
    servingSizeGrams: number;
  };
};

export default function FoodItemPage() {
  const [view, setView] = useState<"search" | "add">("search");

  const handleAddFoodItem = async (item: FoodItemType) => {
    const apiUrl = import.meta.env.VITE_API_URL;
    if (!apiUrl) {
      toast.error("API URL is not configured.");
      return;
    }

    try {
      const response = await fetch(`${apiUrl}/food-items`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(item)
      });

      if (!response.ok) {
        throw new Error("Failed to add food item");
      }

      const result = await response.json();
      console.log("Food item successfully added:", result);
      toast.success("Food item added successfully!");
      setView("search");
    } catch (error) {
      console.error("Error adding food item:", error);
      toast.error("Failed to add food item. Please try again.");
    }
  };

  return (
    <div className="max-w-5xl mx-auto p-6 space-y-6">
      <div className="flex flex-wrap items-center gap-60">
        <BackButton />

        <div className="flex gap-4 ml-4">
          <button
            onClick={() => setView("search")}
            className={`px-4 py-2 rounded-md ${
              view === "search" ? "bg-green-600 text-white" : "bg-gray-200"
            }`}
          >
            Search
          </button>
          <button
            onClick={() => setView("add")}
            className={`px-4 py-2 rounded-md ${
              view === "add" ? "bg-green-600 text-white" : "bg-gray-200"
            }`}
          >
            Add Food Item
          </button>
        </div>
      </div>

      {view === "search" ? (
        <SearchFoodItems />
      ) : (
        <AddFoodItem onAdd={handleAddFoodItem} />
      )}
    </div>
  );
}