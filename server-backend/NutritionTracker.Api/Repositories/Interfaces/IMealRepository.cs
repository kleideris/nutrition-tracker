using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories.Interfaces
{
    /// <summary>
    /// Defines data access operations for retrieving Meal entities, including user-specific queries and optional date filtering.
    /// </summary>
    public interface IMealRepository
    {
        /// <summary>
        /// Retrieves all meals associated with a specific user by their unique identifier including its food items and nutritional data.
        /// </summary>
        Task<IEnumerable<Meal>> GetByUserAsync(int UserId);
        

        //WIP
        //Task<IEnumerable<Meal>> GetMealsByDateRangeAsync(int UserId, DateTime startDate, DateTime endDate);

    }
}
