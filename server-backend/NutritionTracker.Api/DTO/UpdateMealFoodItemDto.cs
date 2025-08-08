using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.DTO
{
    public class UpdateMealFoodItemDto
    {
        public float Quantity { get; set; }
        public string Unit { get; set; } = "grams";
    }
}
