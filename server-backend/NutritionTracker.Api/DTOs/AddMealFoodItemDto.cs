namespace NutritionTracker.Api.DTOs
{
    public class AddMealFoodItemDto
    {
        public float Quantity { get; set; }
        public string Unit { get; set; } = "grams";
    }
}
