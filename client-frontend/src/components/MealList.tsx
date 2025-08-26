import { useEffect, useState, useContext } from "react";
import { AuthContext } from "@/context/AuthContext";
import { fetchWithAuth } from "@/api/fetchWithAuth";
import { toast } from "sonner";
import { FaBreadSlice, FaCheese, FaDrumstickBite, FaEdit, FaFireAlt, FaTrashAlt } from "react-icons/fa";
import { LogMealForm } from "./LogMealForm";
import { Icons, iconStyles } from "./icons";

interface Meal {
  id: number;
  mealType: string;
  timestamp: string;
  totalCalories: number;
  totalProtein: number;
  totalCarbs: number;
  totalFats: number;
  foodItems: {
    foodItemId: number;
    quantity: number;
    unitOfMeasurement: string;
    foodName?: string;
    calories?: number;
    protein?: number;
    carbs?: number;
    fats?: number;
  }[];
}

const MealList = () => {
  const auth = useContext(AuthContext);
  const userId = auth?.user?.id;
  const [meals, setMeals] = useState<Meal[]>([]);
  const [editingMeal, setEditingMeal] = useState<Meal | null>(null);

  const fetchMeals = async () => {
    if (!userId) return;
    try {
      const res = await fetchWithAuth(`/meals/user-id/${userId}`);
      if (res.ok) {
        const data: Meal[] = await res.json();
        const sortedMeals = data.sort(
          (a, b) => new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime()
        );
        setMeals(sortedMeals);
      } else {
        toast.error("Failed to fetch meals.");
      }
    } catch (error) {
      toast.error("An error occurred while fetching meals.");
      console.error(error);
    }
  };

  const deleteMeal = async (mealId: number) => {
    try {
      const res = await fetchWithAuth(`/meals/${mealId}`, {
        method: "DELETE",
      });
      if (res.ok) {
        toast.success("Meal deleted successfully.");
        setMeals((prev) => prev.filter((meal) => meal.id !== mealId));
      } else {
        toast.error("Failed to delete meal.");
      }
    } catch (error) {
      toast.error("An error occurred while deleting meal.");
      console.error(error);
    }
  };

  useEffect(() => {
    fetchMeals();
  }, [userId]);

  return (
    <div>
      {editingMeal && (
        <div className="fixed inset-0 backdrop-blur-sm bg-white/10 flex justify-center items-center z-50">
          <div className="bg-white p-6 rounded-lg w-full max-w-2xl">
            <LogMealForm
              mode="edit"
              initialMeal={editingMeal}
              onSuccess={() => {
                setEditingMeal(null);
                fetchMeals();
              }}
            />
            <button
              onClick={() => setEditingMeal(null)}
              className="mt-4 text-sm text-gray-500 hover:underline"
            >
              Cancel
            </button>
          </div>
        </div>
      )}

      {meals.length === 0 ? (
        <p>No meals logged yet.</p>
      ) : (
        <ul>
          {meals.map((meal) => (
            <li key={meal.id} className="mb-4 p-4 border rounded bg-gray-50">
              <div className="flex justify-between items-center">
                <div>
                  <div className="font-bold text-lg">{meal.mealType}</div>
                  <div className="text-sm text-gray-600">
                    {new Date(meal.timestamp).toLocaleString()}
                  </div>
                </div>
                <div className="space-x-2">
                  <button
                    onClick={() => setEditingMeal(meal)}
                    aria-label="Edit meal"
                  >
                    <Icons.Edit />
                  </button>
                  <button
                    onClick={() => deleteMeal(meal.id)}
                    aria-label="Delete meal"
                  >
                    <Icons.Delete />
                  </button>
                </div>
              </div>

              <div className="mt-2 text-sm flex flex-wrap gap-4 items-center">
                <span className="flex items-center gap-1"><Icons.Calories /> {meal.totalCalories.toFixed(1)} kcal</span>
                <span className="flex items-center gap-1"><Icons.Protein /> {meal.totalProtein.toFixed(1)}g protein</span>
                <span className="flex items-center gap-1"><Icons.Carbs /> {meal.totalCarbs.toFixed(1)}g carbs</span>
                <span className="flex items-center gap-1"><Icons.Fats /> {meal.totalFats.toFixed(1)}g fat</span>
              </div>

              {meal.foodItems?.length > 0 ? (
                <ul className="mt-2 text-sm text-gray-700 space-y-1">
                  {meal.foodItems.map((item, index) => (
                    <li key={index} className="ml-4 list-disc">
                      {item.foodName ?? `Food #${item.foodItemId}`} – {item.quantity} {item.unitOfMeasurement}
                      {/* Nutrition for each individual food item in every meal */}
                      {/* {item.calories && (
                        <span className="text-xs text-gray-500 flex gap-2 flex-wrap items-center">
                          <span className="flex items-center gap-1"><Icons.Calories className={iconStyles.gray} /> {item.calories} kcal</span>
                          <span className="flex items-center gap-1"><Icons.Protein className={iconStyles.gray} /> {item.protein}g</span>
                          <span className="flex items-center gap-1"><Icons.Calories className={iconStyles.gray} /> {item.carbs}g</span>
                          <span className="flex items-center gap-1"><Icons.Fats className={iconStyles.gray} /> {item.fats}g</span>
                        </span>
                      )} */}
                    </li>
                  ))}
                </ul>
              ) : (
                <div className="text-sm text-gray-500 mt-2">No food items logged.</div>
              )}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default MealList;