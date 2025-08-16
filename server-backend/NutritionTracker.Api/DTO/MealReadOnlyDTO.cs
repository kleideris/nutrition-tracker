using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTO
{
    public class MealReadOnlyDto
    {
        public MealType MealType { get; set; }
        public DateTime Timestamp { get; set; }
        public List<MealFoodItemDto> FoodItems { get; set; } = [];

        // Aggregated nutrition
        public float TotalCalories { get; set; }
        public float TotalProtein { get; set; }
        public float TotalCarbs { get; set; }
        public float TotalFats { get; set; }

    }
}
