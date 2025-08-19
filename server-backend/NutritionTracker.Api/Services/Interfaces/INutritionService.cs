using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTOs;

namespace NutritionTracker.Api.Services.Interfaces
{
    /// <summary>
    /// Provides methods for calculating nutritional data based on meal composition.
    /// </summary>
    public interface INutritionService
    {

        /// <summary>
        /// Calculates the nutritional values (e.g., calories, protein, carbs, fat) for a specific food item consumed in a meal.
        /// </summary>
        FoodNutritionDto CalculateNutrition(MealFoodItem mealFoodItem);


        /// <summary>
        /// Aggregates nutritional values for all food items in a meal and returns a summary of total nutrition.
        /// </summary>
        MealNutritionDto CalculateNutrition(Meal meal);
    }
}
