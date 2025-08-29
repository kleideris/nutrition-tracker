import type { FoodItemDto } from "./foodItemDto";

export interface SelectedItem extends FoodItemDto {
  quantity: number;
  unit: string;
}

export interface MealFormProps {
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