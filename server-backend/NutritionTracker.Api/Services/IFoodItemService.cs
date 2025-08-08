using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;

namespace NutritionTracker.Api.Services
{
    public interface IFoodItemService
    {
        Task<FoodItem?> GetFoodItemByIdAsync(int id);
        Task<List<FoodItem?>> GetAllFoodItemsAsync();
        Task<bool> AddFoodItemAsync(FoodItemDto dto);

        

        //Task<IEnumerable<FoodItem>> SearchFoodItemsAsync(string query);
        //Task<bool> UpdateFoodItemAsync(FoodItem foodItem);
        //Task<bool> DeleteFoodItemAsync(int id);
        //Task<NutritionInfo?> GetNutritionInfoAsync(int foodItemId);
        //Task<NutritionInfo> CalculateNutritionAsync(int foodItemId, double quantity, string unit);
    }
}
