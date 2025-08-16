using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public class MealFoodItemRepository(AppDBContext context) : BaseRepository<MealFoodItem>(context), IMealFoodItemRepository
    {


        //WIP
        public async Task<bool> ExistsAsync(int mealId, int foodItemId)
        {
            return await context.MealFoodItems
                .AnyAsync(mfi => mfi.MealId == mealId && mfi.FoodItemId == foodItemId);
        }


        //WIP
        public async Task<MealFoodItem?> GetByJoinedIdsAsync(int mealId, int foodItemId)
        {
            return await context.MealFoodItems
                .FirstOrDefaultAsync(mfi => mfi.MealId == mealId && mfi.FoodItemId == foodItemId);
        }


        //WIP
        public async Task<IEnumerable<MealFoodItem>> GetByMealIdAsync(int mealId)
        {
            return await context.MealFoodItems
                .Where(mfi => mfi.MealId == mealId)
                .Include(mfi => mfi.FoodItem)
                .ToListAsync();
        }


        //WIP
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
