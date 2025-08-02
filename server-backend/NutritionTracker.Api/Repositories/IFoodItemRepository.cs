using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public interface IFoodItemRepository
    {
        Task<FoodItem?> GetByNameAsync(string name);


        //Task UpdateAsync(FoodItem item);
        //Task DeleteAsync(int id);
        //Task<IEnumerable<FoodItem>> SearchByKeywordAsync(string keyword);


    }
}
