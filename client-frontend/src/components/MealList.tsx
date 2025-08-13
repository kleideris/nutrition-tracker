import { useEffect, useState } from "react";
import { fetchWithAuth } from "@/api/fetchWithAuth";
import BackButton from "@/components/BackButton";
import { toast } from "sonner";

export default function MealList() {
  const [meals, setMeals] = useState([]);

  useEffect(() => {
    const fetchMeals = async () => {
      const res = await fetchWithAuth("/meals/user-id/1");
      if (res.ok) {
        const data = await res.json();
        setMeals(data);
      } else {
        toast.error("Failed to fetch meals.");
      }
    };
    fetchMeals();
  }, []);

  return (
    <div className="max-w-2xl mx-auto bg-white p-8 rounded-lg shadow">
      <BackButton />
      <h2 className="text-2xl font-semibold mb-6 border-b pb-2">📋 My Meals</h2>
      <ul className="space-y-4">
        {meals.map((meal: any) => (
          <li
            key={meal.id}
            className="p-4 border border-gray-200 rounded bg-gray-50"
          >
            <div className="font-bold">{meal.mealType}</div>
            <div className="text-sm text-gray-600">
              {new Date(meal.timestamp).toLocaleDateString()}
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}