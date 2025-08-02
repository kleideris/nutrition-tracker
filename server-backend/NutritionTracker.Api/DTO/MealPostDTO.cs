using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTO
{
    public class MealPostDTO
    {
        public int UserId { get; set; }
        public MealType MealType { get; set; }
        public DateTime Timestamp { get; set; }
        public List<MealFoodItemDTO> FoodItems { get; set; } = new();

    }

    public class MealFoodItemDTO
    {
        public int FoodItemId { get; set; }
        public float Quantity { get; set; }
        public string? UnitOfMeasurement { get; set; } = "grams";
    }
}
