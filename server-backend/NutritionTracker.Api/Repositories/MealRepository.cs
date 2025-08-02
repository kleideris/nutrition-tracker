using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public class MealRepository : BaseRepository<Meal>, IMealRepository
    {
        public MealRepository(AppDBContext context) : base(context)
        {
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
