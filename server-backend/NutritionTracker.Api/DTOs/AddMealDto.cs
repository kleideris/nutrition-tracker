using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTOs
{
    public class AddMealDto
    {
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public List<MealFoodItemDto> MealFoodItems { get; set; } = [];

    }
}
