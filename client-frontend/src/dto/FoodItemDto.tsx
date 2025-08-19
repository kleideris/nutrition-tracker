export interface NutritionDataDto {
  calories: number;
  fats: number;
  carbohydrates: number;
  protein: number;
  foodItemId?: number;
  servingSizeGrams: number;
}

export interface FoodItemDto {
  id?: number;
  name: string;
  nutritionData: NutritionDataDto;
}