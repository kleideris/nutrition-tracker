using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;

namespace NutritionTracker.Api.Services
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetMealsByUserAsync(int userId);
        Task<Meal?> GetMealByIdAsync(int mealId);
        Task<bool> AddMealAsync(MealPostDTO dto);


        //Task<IEnumerable<Meal>> GetMealsByDateRangeAsync(int UserId, DateTime startDate, DateTime endDate);
        //Task<bool> UpdateMealAsync(int mealId, MealCreateDTO dto);
        //Task<bool> DeleteMealAsync(int mealId);

    }
}
