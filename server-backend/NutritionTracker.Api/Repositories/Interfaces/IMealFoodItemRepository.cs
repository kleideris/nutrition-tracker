using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories.Interfaces
{
    /// <summary>
    /// Defines data access operations for managing MealFoodItem entities,
    /// including retrieval, existence checks, and quantity updates.
    /// </summary>
    public interface IMealFoodItemRepository
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
        /// Checks whether a specific MealFoodItem entry exists for the given meal and food item IDs.
        /// </summary>
        Task<bool> ExistsAsync(int mealId, int foodItemId);


        /// <summary>
        /// Updates the quantity and unit of measurement for a specific food item in a meal.
        /// </summary>
        Task<bool> UpdateQuantityAsync(int emalId, int foodItemId, float quantity, string unit);
    }
}
