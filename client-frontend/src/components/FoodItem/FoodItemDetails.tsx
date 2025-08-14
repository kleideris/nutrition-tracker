import { type FoodItemDto } from "@/dto/FoodItemDto";

export const FoodItemDetails = ({
  item,
  onClose,
}: {
  item: FoodItemDto;
  onClose: () => void;
}) => (
  <div>
    <button
      onClick={onClose}
      className="text-sm text-red-500 hover:underline float-right"
    >
      Close ✖
    </button>
    <h2 className="text-2xl font-bold text-green-700 mb-2">{item.name}</h2>
    <ul className="space-y-1 text-gray-700">
      <li>Calories: {item.nutritionData.calories} kcal</li>
      <li>Fats: {item.nutritionData.fats} g</li>
      <li>Carbohydrates: {item.nutritionData.carbohydrates} g</li>
      <li>Protein: {item.nutritionData.protein} g</li>
      <li>Serving Size: {item.nutritionData.servingSizeGrams} g</li>
    </ul>
  </div>
);