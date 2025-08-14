import {type FoodItemDto } from "@/dto/FoodItemDto";

export const FoodItemCard = ({
  item,
  onClick,
}: {
  item: FoodItemDto;
  onClick: () => void;
}) => (
  <div
    onClick={onClick}
    className="cursor-pointer bg-white rounded-lg shadow-md hover:shadow-xl transition p-4 border border-green-200"
  >
    <h2 className="text-xl font-semibold text-green-800">{item.name}</h2>
    <p className="text-sm text-gray-600">Calories: {item.nutritionData.calories} kcal</p>
    <p className="text-sm text-gray-600">Protein: {item.nutritionData.protein} g</p>
  </div>
);