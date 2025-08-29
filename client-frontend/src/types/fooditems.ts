export type FoodItemType = {
  name: string;
  nutritionData: {
    calories: number;
    protein: number;
    carbohydrates: number;
    fats: number;
    servingSizeGrams: number;
  };
};
