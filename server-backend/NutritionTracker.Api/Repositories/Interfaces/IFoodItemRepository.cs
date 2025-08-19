using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories.Interfaces
{
    public interface IFoodItemRepository
    {
        Task<FoodItem?> GetByNameAsync(string name);

        Task<List<FoodItem>> GetAllAsListAsync();

        Task<List<FoodItem>> SearchByNameAsync(string query);

        //Task UpdateAsync(FoodItem item);

        //Task DeleteAsync(int id);
    }
}
