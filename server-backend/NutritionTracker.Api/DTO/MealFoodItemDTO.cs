namespace NutritionTracker.Api.DTO
{
    public class MealFoodItemDto
    {
        public int FoodItemId { get; set; }
        public float Quantity { get; set; }
        public string? UnitOfMeasurement { get; set; } = "grams";
    }
}
