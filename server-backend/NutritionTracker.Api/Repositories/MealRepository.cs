using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public class MealRepository(AppDBContext context) : BaseRepository<Meal>(context), IMealRepository
    {

        //need to drop this and refactor the controllers
        public async Task<bool> IsMealTypeLoggedForDayAsync(int userId, MealType mealType, DateTime date)
        {
            return await dbset.AnyAsync(m =>
                m.UserId == userId &&
                m.MealType == mealType &&
                m.Timestamp.Date == date.Date);
        }


        public override async Task<Meal?> GetAsync(int mealId)
        {
            return await context.Meals
                .Include(m => m.MealFoodItems)
                    .ThenInclude(mfi => mfi.FoodItem)
                        .ThenInclude(fi => fi.NutritionData)
                .FirstOrDefaultAsync(m => m.Id == mealId);
        }


        //public async Task AddMealAsync(Meal meal) => await context.Meals.AddAsync(meal);


        //public async Task<Meal?> GetMealByIdAsync(int mealId) => await context.Meals.FindAsync(mealId);


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
