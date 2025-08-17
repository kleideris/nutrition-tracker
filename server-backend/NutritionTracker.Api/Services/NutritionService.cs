using AutoMapper;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTOs;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Repositories.Interfaces;
using NutritionTracker.Api.Services.Interfaces;
using Serilog;

namespace NutritionTracker.Api.Services
{
    public class NutritionService : INutritonService
    {


        public MealNutritionDto CalculateNutrition(Meal meal)
        {
            var items = meal.MealFoodItems.Select(CalculateNutriton).ToList();

            var totalCalories = items.Sum(i => i.Calories);
            var totalProtein = items.Sum(i => i.Protein);
            var totalCarbs = items.Sum(i => i.Carbs);
            var totalFat = items.Sum(i => i.Fat);

            return new MealNutritionDto
            {
                MealId = meal.Id,
                Items = items,
                TotalCalories = totalCalories,
                TotalProtein = totalProtein,
                TotalCarbs = totalCarbs,
                TotalFat = totalFat
            };

        }


        public FoodNutritionDto CalculateNutriton(MealFoodItem mealFoodItem)
        {
            if (mealFoodItem == null)
                throw new EntityNotFoundException("MealFoodItem", "MealFoodItem was not found.");

            if (mealFoodItem.FoodItem == null)
                throw new EntityNotFoundException("FoodItem", $"FoodItem with id {mealFoodItem.FoodItemId} was not found.");

            if (mealFoodItem.FoodItem.NutritionData == null)
                throw new EntityNotFoundException("NutritionData", $"NutritionData for FoodItem with id {mealFoodItem.FoodItemId} was not found.");

            var food = mealFoodItem.FoodItem;
            var quantity = mealFoodItem.Quantity;

            return new FoodNutritionDto
            {
                FoodName = food.Name,
                QuantityInGrams = quantity,

                Calories = (quantity / 100.0) * food.NutritionData.Calories,
                Protein = (quantity / 100.0) * food.NutritionData.Protein,
                Carbs = (quantity / 100.0) * food.NutritionData.Carbohydrates,
                Fat = (quantity / 100.0) * food.NutritionData.Fats
            };
        }
    }
}
