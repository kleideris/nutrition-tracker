using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTO
{
    public class MealReadOnlyDto
    {
        public MealType MealType { get; set; }
        public DateTime Timestamp { get; set; }
        public List<MealFoodItemDto> FoodItems { get; set; } = new();

    }
}
