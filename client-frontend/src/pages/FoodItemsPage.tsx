import { useState } from "react";
import { SearchFoodItems } from "../components/FoodItem/SearchFoodItems";
import AddFoodItem from "../components/FoodItem/AddFoodItem";
import RemoveFoodItem from "@/components/FoodItem/RemoveFoodItem";
import DashboardContentWrapper from "@/components/DashboardContentWrapper";
import { toast } from "sonner";
import { fetchWithAuth } from "@/api/fetchWithAuth";
import { useAuth } from "@/hooks/useAuth";
import type { FoodItemType } from "@/types/fooditems";


export default function FoodItemPage() {
  const [view, setView] = useState<"search" | "add" | "remove">("search");
  const apiUrl = import.meta.env.VITE_API_URL;
  
  const { user } = useAuth();
  const isAdmin = user?.role === "Admin"

  const handleAddFoodItem = async (item: FoodItemType) => {
    if (!apiUrl) {
      toast.error("API URL is not configured.");
      return;
    }

    try {
      const response = await fetchWithAuth(`/food-items`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(item),
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
    <DashboardContentWrapper>
      <div className="flex flex-wrap items-center gap-60">

        <div className="flex gap-4 ml-4">
          {isAdmin && (
            <>
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
              <button
                onClick={() => setView("remove")}
                className={`px-4 py-2 rounded-md ${
                  view === "remove" ? "bg-red-600 text-white" : "bg-gray-200"
                }`}
              >
                Remove Food Item
              </button>
            </>
          )}
        </div>
          
      </div>

      {view === "search" ? (
        <SearchFoodItems />
      ) : view === "add" ? (
        <AddFoodItem onAdd={handleAddFoodItem} />
      ) : (
        <RemoveFoodItem apiUrl={apiUrl} />
      )}
    </DashboardContentWrapper>
  );
}