using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Services
{
    public interface IMealFoodItemService
    {
        Task<MealFoodItem?> GetByJoinedIdsAsync(int mealId, int foodItemId);
        Task<IEnumerable<MealFoodItem>> GetByMealIdAsync(int mealId);
        Task<bool> AddFoodItemToMealAsync(int mealId, int foodItemId, float quantity, string unit);
        Task<bool> DeleteFoodItemOfMealAsync(int mealId, int foodItemId, float quantity, string unit);
        Task<bool> UpdateQuantityOfFoodItemAsync(int mealId, int foodItemId, float quantity, string unit);
    }
}
