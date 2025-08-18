namespace NutritionTracker.Api.Data
{
    public class MealFoodItem :BaseEntity
    {
        /// <summary>
        /// Primary key that uniquely identifies the relationship between a specific meal and its food item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key linking to the associated meal.
        /// </summary>
        public int MealId { get; set; }

        /// <summary>
        /// Foreign key linking to the associated food item.
        /// </summary>
        public int FoodItemId { get; set; }

        /// <summary>
        /// The quantity of food consumed (e.g., "3" eggs, "20.5" kg chicken).
        /// </summary>
        public float Quantity { get; set; }

        // The unit used to measure the food (e.g., "pieces", "cups", "kg").
        // Defaults to "grams".
        public string? UnitOfMeasurement { get; set; } = "grams";

        /// <summary>
        /// Navigation property for the linked meal.
        /// Each MealFoodItem entry belongs to exactly one meal.
        /// </summary>
        public virtual Meal Meal { get; set; } = null!;

        /// <summary>
        /// Each MealFoodItem entry references exactly one food item.
        /// Navigation property for the linked food item.
        /// </summary>
        public virtual FoodItem FoodItem { get; set; } = null!;

    }
}

