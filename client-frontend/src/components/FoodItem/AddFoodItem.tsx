import React, { useState } from "react";
import { type FoodItemDto } from "../../dto/FoodItemDto";
import FoodItemForm from "./FoodItemForm";

type Props = {
  onAdd: (item: FoodItemDto) => void;
};

export default function AddFoodItem({ onAdd }: Props) {
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