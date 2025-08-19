using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTOs;

namespace NutritionTracker.Api.Services
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetMealsByUserAsync(int userId);
        Task<Meal?> GetMealByIdAsync(int mealId);
        Task<bool> AddMealAsync(MealType mealType, AddMealDto dto);


        //Task<IEnumerable<Meal>> GetMealsByDateRangeAsync(int UserId, DateTime startDate, DateTime endDate);
        //Task<bool> UpdateMealAsync(int mealId, MealCreateDTO dto);
        //Task<bool> DeleteMealAsync(int mealId);

    }
}
