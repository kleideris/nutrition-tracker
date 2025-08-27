using NutritionTracker.Api.Data;
using NutritionTracker.Api.Repositories.Interfaces;

namespace NutritionTracker.Api.Repositories
{
    /// <summary>
    /// Implements the <see cref="IUnitOfWork"/> interface to coordinate repository operations
    /// using a shared <see cref="AppDBContext"/> for transactional consistency.
    /// </summary>
    public class UnitOfWork(AppDBContext context) : IUnitOfWork
    {
        private readonly AppDBContext _context = context;

        public UserRepository UserRepository => new (_context);
        public MealRepository MealRepository => new (_context);
        public MealFoodItemRepository MealFoodItemRepository => new (_context);
        public FoodItemRepository FoodItemRepository => new(_context);

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;
    }
}
