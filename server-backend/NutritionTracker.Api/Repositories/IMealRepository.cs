using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public interface IMealRepository
    {
        Task<IEnumerable<Meal>> GetMealsByUserAsync(int UserId);
        Task<Meal?> GetMealByIdAsync(int mealId);
        Task AddMealAsync(Meal meal);


        //Task<IEnumerable<Meal>> GetMealsByDateRangeAsync(int UserId, DateTime startDate, DateTime endDate);
        //Task UpdateMealAsync(Meal meal);
        //Task DeleteMealAsync(int mealId);

    }
}
