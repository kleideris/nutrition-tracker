using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public interface IFoodItemRepository
    {
        Task<FoodItem?> GetFoodItemByNameAsync(string name);
        Task<IEnumerable<FoodItem>> SearchByKeywordAsync(string keyword);
    }
}
