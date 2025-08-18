using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.DTOs
{
    public class UpdateMealFoodItemDto
    {
        public float Quantity { get; set; }
        public string Unit { get; set; } = "grams";
    }
}
