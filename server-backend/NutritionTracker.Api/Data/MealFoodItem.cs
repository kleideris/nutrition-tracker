namespace NutritionTracker.Api.Data
{
    public class MealFoodItem :BaseEntity
    {
        // Primary key that uniquely identifies the relationship between a specific meal and its food item.
        public int Id { get; set; }

        // Foreign key linking to the associated meal.
        public int MealId { get; set; }

        // Foreign key linking to the associated food item.
        public int FoodItemId { get; set; }

        // The quantity of food consumed (e.g., "3" eggs, "20.5" kg chicken).
        public float Quantity { get; set; }

        // The unit used to measure the food (e.g., "pieces", "cups", "kg").
        // Defaults to "grams".
        public string? UnitOfMeasurement { get; set; } = "grams";

        // Navigation property for the linked meal.
        // Each MealFoodItem entry belongs to exactly one meal.
        public virtual Meal Meal { get; set; } = null!;

        // Navigation property for the linked food item.
        // Each MealFoodItem entry references exactly one food item.
        public virtual FoodItem FoodItem { get; set; } = null!;

    }
}

