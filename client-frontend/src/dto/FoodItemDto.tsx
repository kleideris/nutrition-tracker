export interface NutritionDataDto {
  calories: number;
  fats: number;
  carbohydrates: number;
  protein: number;
  foodItemId?: number;
  servingSizeGrams: number;
}

export interface FoodItemDto {
  name: string;
  nutritionData: NutritionDataDto;
}