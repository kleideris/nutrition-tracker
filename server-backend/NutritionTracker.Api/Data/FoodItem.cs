namespace NutritionTracker.Api.Data
{
    public class FoodItem : BaseEntity
    {
        // Primary key used to uniquely identify each food item in the database.
        public int Id { get; set; }

        // The display name of the food item (e.g., "Banana", "Grilled Chicken Breast").
        public string Name { get; set; } = null!;

        // Navigation property to associated nutritional data for this food item.
        public virtual NutritionData NutritionData { get; set; } = null!;

        // Navigation property for meal associations.
        // One-to-many: this food item can appear in multiple MealFoodItem join records.
        // Useful for tracking how and where the food was consumed.
        public virtual ICollection<MealFoodItem>? MealFoodItems { get; set; } = new HashSet<MealFoodItem>();
    }
}
