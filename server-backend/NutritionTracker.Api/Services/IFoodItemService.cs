using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTOs;

namespace NutritionTracker.Api.Services
{
    public interface IFoodItemService
    {
        Task<FoodItem?> GetByIdAsync(int id);
        Task<List<FoodItem?>> GetAllAsync();
        Task<bool> AddAsync(FoodItemDto dto);

        

        //Task<IEnumerable<FoodItem>> SearchFoodItemsAsync(string query);
        //Task<bool> UpdateFoodItemAsync(FoodItem foodItem);
        //Task<bool> DeleteFoodItemAsync(int id);
        //Task<NutritionInfo?> GetNutritionInfoAsync(int foodItemId);
        //Task<NutritionInfo> CalculateNutritionAsync(int foodItemId, double quantity, string unit);
    }
}
