using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories.Interfaces
{
    public interface IGoalsRepository
    {
        Task<Goal?> GetActiveGoalAsync(int userId);

        Task<IEnumerable<Goal>> GetGoalHistoryAsync(int userId);
    }
}
