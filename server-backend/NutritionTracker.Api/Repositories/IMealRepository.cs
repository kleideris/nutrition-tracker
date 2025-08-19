using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public interface IMealRepository
    {
        Task<IEnumerable<Meal>> GetByUserAsync(int UserId);
        


        //Task<IEnumerable<Meal>> GetMealsByDateRangeAsync(int UserId, DateTime startDate, DateTime endDate);
        //Task UpdateMealAsync(Meal meal);
        //Task DeleteMealAsync(int mealId);

    }
}
