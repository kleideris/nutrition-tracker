using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public interface IMealRepository
    {
        Task<IEnumerable<Meal>> GetMealsByUserAsync(int UserId);
        Task<IEnumerable<Meal>> GetMealsByDateRangeAsync(int UserId, DateTime startDate, DateTime endDate);
    }
}
