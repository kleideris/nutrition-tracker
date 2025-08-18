namespace NutritionTracker.Api.DTOs
{
    public class MealFoodItemDto
    {
        public int FoodItemId { get; set; }
        public float Quantity { get; set; }
        public string? UnitOfMeasurement { get; set; } = "grams";

        public string? FoodName { get; set; }
        public float? Calories { get; set; }
        public float? Protein { get; set; }
        public float? Carbs { get; set; }
        public float? Fats { get; set; }

    }
}
