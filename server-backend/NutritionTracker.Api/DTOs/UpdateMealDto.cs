using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTOs
{
    public class UpdateMealDto
    {
        public MealType MealType { get; set; }
        public DateTime Timestamp { get; set; }
        public List<MealFoodItemDto> MealFoodItems { get; set; } = [];
    }
}
