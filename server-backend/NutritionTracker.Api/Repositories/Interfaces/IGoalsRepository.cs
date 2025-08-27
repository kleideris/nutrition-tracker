using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories.Interfaces
{
    /// <summary>
    /// Work in progress, not used by anything so far..
    /// </summary>
    public interface IGoalsRepository
    {
        Task<Goal?> GetActiveGoalAsync(int userId);

        Task<IEnumerable<Goal>> GetGoalHistoryAsync(int userId);
    }
}
