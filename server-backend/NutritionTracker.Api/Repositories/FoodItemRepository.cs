using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public class FoodItemRepository(AppDBContext context) : BaseRepository<FoodItem>(context), IFoodItemRepository
    {

        public override async Task<FoodItem?> GetAsync(int id) => 
            await dbset.Include(f => f.NutritionData).FirstOrDefaultAsync(f => f.Id == id);


        public async Task<List<FoodItem>> GetAllAsListAsync() => 
            await dbset.Include(f => f.NutritionData)
                .OrderBy(f => f.Name)
                .ToListAsync();


        public async Task<FoodItem?> GetByNameAsync(string name) => 
            await context.FoodItems.Include(f => f.NutritionData).FirstOrDefaultAsync(f => f.Name == name);


        public async Task<List<FoodItem>> SearchByNameAsync(string query) =>
            await context.FoodItems
                .Include(f => f.NutritionData)
                .Where(f => f.Name.Contains(query))
                .OrderBy(f => f.Name)
                .ToListAsync();
    }
}
