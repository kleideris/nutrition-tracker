using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public interface IMealFoodItemRepository
    {
        Task<Meal?> GetMealAsync(int mealId);
        Task<IEnumerable<MealFoodItem>> GetMealFoodItemsByMealIdAsync(int mealId);
        Task AddAsync(MealFoodItem link);


        //Task RemoveAsync(int id); // Remove by link ID

    }
}
