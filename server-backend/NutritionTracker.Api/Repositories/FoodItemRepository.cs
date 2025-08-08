using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public class FoodItemRepository : BaseRepository<FoodItem>, IFoodItemRepository
    { 
        public FoodItemRepository(AppDBContext context) : base(context)
        {
        }



        //Finished
        public override async Task<FoodItem?> GetAsync(int id) => 
            await dbset.Include(f => f.NutritionData).FirstOrDefaultAsync(f => f.Id == id);


        //Finished
        public async Task<List<FoodItem>> GetAllAsListAsync() => 
            await dbset.Include(f => f.NutritionData).ToListAsync();


        //Finished
        public async Task<FoodItem?> GetByNameAsync(string name) => 
            await context.FoodItems.Include(f => f.NutritionData).FirstOrDefaultAsync(f => f.Name == name);
    }
}
