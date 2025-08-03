using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public class MealFoodItemRepository : BaseRepository<MealFoodItem>, IMealFoodItemRepository
    {
        public MealFoodItemRepository(AppDBContext context) : base(context)
        {
        }



        //WIP
        public Task<Meal?> GetMealAsync(int mealId)
        {
            throw new NotImplementedException();
        }


        //WIP
        public Task<IEnumerable<MealFoodItem>> GetMealFoodItemsByMealIdAsync(int mealId)
        {
            throw new NotImplementedException();
        }
    }
}
