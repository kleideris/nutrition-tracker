using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Services.Interfaces
{
    /// <summary>
    /// Provides operations for managing the relationship between meals and food items,
    /// including adding, updating, and removing items from a meal.
    /// </summary>
    public interface IMealFoodItemService
    {

        /// <summary>
        /// Retrieves a specific MealFoodItem entry based on the combination of meal and food item IDs.
        /// </summary>
        Task<MealFoodItem?> GetByJoinedIdsAsync(int mealId, int foodItemId);


        /// <summary>
        /// Retrieves all MealFoodItem entries associated with a specific meal.
        /// </summary>
        Task<IEnumerable<MealFoodItem>> GetByMealIdAsync(int mealId);


        /// <summary>
        /// Adds a food item to a meal with the specified quantity and unit of measurement.
        /// </summary>
        Task<bool> AddFoodItemToMealAsync(int mealId, int foodItemId, float quantity, string unit);


        /// <summary>
        /// Removes a food item from a meal based on the specified quantity and unit.
        /// </summary>
        Task<bool> DeleteFoodItemOfMealAsync(int mealId, int foodItemId, float quantity, string unit);


        /// <summary>
        /// Updates the quantity and unit of measurement for a specific food item in a meal.
        /// </summary>
        Task<bool> UpdateQuantityOfFoodItemAsync(int mealId, int foodItemId, float quantity, string unit);
    }
}
