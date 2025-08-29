import React, { useState } from "react";
import { type FoodItemDto } from "../../types/foodItemDto";
import FoodItemForm from "./FoodItemForm";
import type { AddFoodItemProps } from "@/types/foodItemForm";

export default function AddFoodItem({ onAdd }: AddFoodItemProps) {
  const [formData, setFormData] = useState<FoodItemDto>({
    name: "",
    nutritionData: {
      calories: 0,
      protein: 0,
      carbohydrates: 0,
      fats: 0,
      servingSizeGrams: 0
    }
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onAdd(formData);
  };

  return (
    <FoodItemForm
      formData={formData}
      setFormData={setFormData}
      onSubmit={handleSubmit}
    />
  );
}