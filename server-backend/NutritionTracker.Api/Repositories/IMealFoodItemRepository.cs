using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public interface IMealFoodItemRepository
    {
        Task<MealFoodItem?> GetByJoinedIdsAsync(int mealId, int foodItemId);
        Task<IEnumerable<MealFoodItem>> GetByByMealIdAsync(int mealId);
        Task<bool> ExistsAsync(int mealId, int foodItemId);
        Task<bool> UpdateQuantityAsync(int emalId, int foodItemId, float quantity, string unit);



        //Task RemoveAsync(int id); // Remove by link ID

    }
}
