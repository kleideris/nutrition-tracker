using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTOs;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services.Interfaces;

namespace NutritionTracker.Api.Services
{
    public class NutritionService() : INutritionService
    {

        public MealNutritionDto CalculateNutrition(Meal meal)
        {
            if (meal == null)
                throw new EntityNotFoundException("Meal", "Meal was not provided.");

            if (meal.MealFoodItems == null || meal.MealFoodItems.Count == 0)
                throw new EntityNotFoundException("MealFoodItems", "No food items found for the meal you provided with id: " + meal.Id);

            var items = meal.MealFoodItems.Select(CalculateNutrition).ToList();

            var totalCalories = items.Sum(i => i.Calories);
            var totalProtein = items.Sum(i => i.Protein);
            var totalCarbs = items.Sum(i => i.Carbs);
            var totalFats = items.Sum(i => i.Fat);

            return new MealNutritionDto
            {
                MealId = meal.Id,
                Items = items,
                TotalCalories = totalCalories,
                TotalProtein = totalProtein,
                TotalCarbs = totalCarbs,
                TotalFats = totalFats
            };
        }


        public FoodNutritionDto CalculateNutrition(MealFoodItem mealFoodItem)
        {
            if (mealFoodItem == null)
                throw new EntityNotFoundException("MealFoodItem", "MealFoodItem was not found.");

            var food = mealFoodItem.FoodItem 
                ?? throw new EntityNotFoundException("FoodItem", $"FoodItem with id {mealFoodItem.FoodItemId} was not found.");

            if (food.NutritionData == null)
                throw new EntityNotFoundException("NutritionData", $"NutritionData for FoodItem with id {mealFoodItem.FoodItemId} was not found.");

            var quantity = mealFoodItem.Quantity;

            return new FoodNutritionDto
            {
                FoodName = food.Name,
                QuantityInGrams = quantity,
                Calories = (quantity / 100.0f) * food.NutritionData.Calories,
                Protein = (quantity / 100.0f) * food.NutritionData.Protein,
                Carbs = (quantity / 100.0f) * food.NutritionData.Carbohydrates,
                Fat = (quantity / 100.0f) * food.NutritionData.Fats
            };
        }
    }
}