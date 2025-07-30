using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public interface IMealFoodItemRepository
    {
        Task<Meal?> GetMealAsync(int mealId);
        Task<IEnumerable<FoodItem>> GetFoodItemsAsync(int mealId);
    }
}
