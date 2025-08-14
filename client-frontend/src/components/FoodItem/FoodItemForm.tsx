import React from "react";
import { type FoodItemDto } from "../../dto/FoodItemDto";

type Props = {
  formData: FoodItemDto;
  setFormData: React.Dispatch<React.SetStateAction<FoodItemDto>>;
  onSubmit: (e: React.FormEvent) => void;
};

export default function FoodItemForm({ formData, setFormData, onSubmit }: Props) {
  return (
    <form
      onSubmit={onSubmit}
      className="bg-white border border-gray-200 rounded-lg p-6 shadow-md grid grid-cols-1 md:grid-cols-2 gap-6"
    >
      {/* Food Name */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Food Name</label>
        <input
          type="text"
          value={formData.name}
          onChange={e => setFormData({ ...formData, name: e.target.value })}
          placeholder="Food name"
          className="w-full border border-gray-300 p-3 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-green-400"
          required
        />
      </div>

      {/* Calories */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Calories</label>
        <input
          type="number"
          value={formData.nutritionData.calories}
          onChange={e =>
            setFormData({
              ...formData,
              nutritionData: {
                ...formData.nutritionData,
                calories: +e.target.value
              }
            })
          }
          placeholder="Calories"
          className="w-full border border-gray-300 p-3 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-green-400"
          required
        />
      </div>

      {/* Protein */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Protein (g)</label>
        <input
          type="number"
          value={formData.nutritionData.protein}
          onChange={e =>
            setFormData({
              ...formData,
              nutritionData: {
                ...formData.nutritionData,
                protein: +e.target.value
              }
            })
          }
          placeholder="Protein"
          className="w-full border border-gray-300 p-3 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-green-400"
          required
        />
      </div>

      {/* Carbohydrates */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Carbohydrates (g)</label>
        <input
          type="number"
          value={formData.nutritionData.carbohydrates}
          onChange={e =>
            setFormData({
              ...formData,
              nutritionData: {
                ...formData.nutritionData,
                carbohydrates: +e.target.value
              }
            })
          }
          placeholder="Carbohydrates"
          className="w-full border border-gray-300 p-3 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-green-400"
          required
        />
      </div>

      {/* Fats */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Fats (g)</label>
        <input
          type="number"
          value={formData.nutritionData.fats}
          onChange={e =>
            setFormData({
              ...formData,
              nutritionData: {
                ...formData.nutritionData,
                fats: +e.target.value
              }
            })
          }
          placeholder="Fats"
          className="w-full border border-gray-300 p-3 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-green-400"
          required
        />
      </div>

      {/* Serving Size */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Serving Size (g)</label>
        <input
          type="number"
          value={formData.nutritionData.servingSizeGrams}
          onChange={e =>
            setFormData({
              ...formData,
              nutritionData: {
                ...formData.nutritionData,
                servingSizeGrams: +e.target.value
              }
            })
          }
          placeholder="Serving Size"
          className="w-full border border-gray-300 p-3 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-green-400"
          required
        />
      </div>

      {/* Submit Button */}
      <div className="md:col-span-2">
        <button
          type="submit"
          className="w-full bg-green-500 text-white px-4 py-3 rounded-md hover:bg-green-600 transition-colors"
        >
          Add Food Item
        </button>
      </div>
    </form>
  );
}