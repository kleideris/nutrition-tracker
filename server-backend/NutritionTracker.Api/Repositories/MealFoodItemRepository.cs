using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.Repositories.Interfaces;

namespace NutritionTracker.Api.Repositories
{
    /// <summary>
    /// Repository for performing data operations related to <see cref="MealFoodItem"/> entities.
    /// </summary>
    public class MealFoodItemRepository(AppDBContext context) : BaseRepository<MealFoodItem>(context), IMealFoodItemRepository
    {
        #region docs
        /// <summary>
        /// Checks whether a specific food item is already associated with a given meal.
        /// </summary>
        /// <param name="mealId">The unique identifier of the meal.</param>
        /// <param name="foodItemId">The unique identifier of the food item.</param>
        /// <returns>True if the association exists; otherwise, false.</returns>
        #endregion
        public async Task<bool> ExistsAsync(int mealId, int foodItemId)
        {
            return await context.MealFoodItems
                .AnyAsync(mfi => mfi.MealId == mealId && mfi.FoodItemId == foodItemId);
        }


        #region docs
        /// <summary>
        /// Retrieves a specific <see cref="MealFoodItem"/> entity based on both meal and food item identifiers.
        /// </summary>
        /// <param name="mealId">The unique identifier of the meal.</param>
        /// <param name="foodItemId">The unique identifier of the food item.</param>
        /// <returns>The matching <see cref="MealFoodItem"/> entity, or null if not found.</returns>
        #endregion
        public async Task<MealFoodItem?> GetByJoinedIdsAsync(int mealId, int foodItemId)
        {
            return await context.MealFoodItems
                .FirstOrDefaultAsync(mfi => mfi.MealId == mealId && mfi.FoodItemId == foodItemId);
        }


        #region docs
        /// <summary>
        /// Retrieves all <see cref="MealFoodItem"/> entities associated with a specific meal, including their related <see cref="FoodItem"/> entities.
        /// </summary>
        /// <param name="mealId">The unique identifier of the meal.</param>
        /// <returns>A collection of <see cref="MealFoodItem"/> entities linked to the meal.</returns>
        #endregion
        public async Task<IEnumerable<MealFoodItem>> GetByMealIdAsync(int mealId)
        {
            return await context.MealFoodItems
                .Where(mfi => mfi.MealId == mealId)
                .Include(mfi => mfi.FoodItem)
                .ToListAsync();
        }


        #region docs
        /// <summary>
        /// Updates the quantity and unit of measurement for a specific food item within a meal.
        /// </summary>
        /// <param name="mealId">The unique identifier of the meal.</param>
        /// <param name="foodItemId">The unique identifier of the food item.</param>
        /// <param name="quantity">The new quantity value.</param>
        /// <param name="unit">The unit of measurement (e.g., grams, ounces).</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        #endregion
        public async Task<bool> UpdateQuantityAsync(int mealId, int foodItemId, float quantity, string unit)
        {
            var item = await context.MealFoodItems.FirstOrDefaultAsync(mfi => mfi.MealId == mealId && mfi.FoodItemId == foodItemId);
            if (item == null) return false;

            item.Quantity = quantity;
            item.UnitOfMeasurement = unit;

            context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
