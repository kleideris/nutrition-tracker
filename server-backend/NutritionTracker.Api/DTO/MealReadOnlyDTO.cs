using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTO
{
    public class MealReadOnlyDTO
    {
        public MealType MealType { get; set; }
        public DateTime Timestamp { get; set; }
        public List<MealFoodItemDTO> FoodItems { get; set; } = new();

    }
}
