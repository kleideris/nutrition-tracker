using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.Repositories.Interfaces;

namespace NutritionTracker.Api.Repositories
{
    /// <summary>
    /// Repository for performing data operations related to <see cref="Meal"/> entities.
    /// </summary>
    public class MealRepository(AppDBContext context) : BaseRepository<Meal>(context), IMealRepository
    {
        #region docs
        /// <summary>
        /// Retrieves a single <see cref="Meal"/> entity by its unique identifier, including its associated food items and their nutritional data.
        /// </summary>
        /// <param name="mealId">The unique identifier of the meal.</param>
        /// <returns>The matching <see cref="Meal"/> entity, or null if not found.</returns>
        #endregion
        public override async Task<Meal?> GetAsync(int mealId)
        {
            return await context.Meals
                .Include(m => m.MealFoodItems)
                .ThenInclude(mfi => mfi.FoodItem)
                .ThenInclude(fi => fi.NutritionData)
                .FirstOrDefaultAsync(m => m.Id == mealId);
        }


        #region docs
        /// <summary>
        /// Retrieves all <see cref="Meal"/> entities associated with a specific user, including their food items and nutritional data.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A collection of <see cref="Meal"/> entities belonging to the user.</returns>
        #endregion
        public async Task<IEnumerable<Meal>> GetByUserAsync(int userId)
        {
            return await context.Meals
                .Include(m => m.MealFoodItems)
                .ThenInclude(mfi => mfi.FoodItem)
                .ThenInclude(fi => fi.NutritionData)
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }
    }
}
