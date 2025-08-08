using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public class MealRepository(AppDBContext context) : BaseRepository<Meal>(context), IMealRepository
    {


        //WIP
        public async Task<bool> IsMealTypeLoggedForDayAsync(int userId, MealType mealType, DateTime date)
        {
            return await dbset.AnyAsync(m =>
                m.UserId == userId &&
                m.MealType == mealType &&
                m.Timestamp.Date == date.Date);
        }


        //Finished (Adds meal but need to make it so you can only add 1 type of meal per day...)
        public async Task AddMealAsync(Meal meal) => await context.Meals.AddAsync(meal);


        //Finished with minor bugs
        public async Task<Meal?> GetMealByIdAsync(int mealId) => await context.Meals.FindAsync(mealId);


        //Finished with minor bugs
        public async Task<IEnumerable<Meal>> GetMealsByUserAsync(int userId)
        {
            return await context.Meals
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }
    }
}
