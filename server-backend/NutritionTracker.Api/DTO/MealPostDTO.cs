using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTO
{
    public class MealPostDto
    {
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public List<MealFoodItemDto> MealFoodItems { get; set; } = new();

    }
}
