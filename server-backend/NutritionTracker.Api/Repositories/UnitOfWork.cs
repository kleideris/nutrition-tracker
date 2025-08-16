using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public class UnitOfWork(AppDBContext context) : IUnitOfWork
    {
        private readonly AppDBContext _context = context;

        public UserRepository UserRepository => new (_context);
        public MealRepository MealRepository => new (_context);
        public MealFoodItemRepository MealFoodItemRepository => new (_context);
        public FoodItemRepository FoodItemRepository => new(_context);


        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
