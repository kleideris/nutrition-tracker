namespace NutritionTracker.Api.Data
{
    public class FoodItem
    {
        // Primary key used to uniquely identify each food item in the database.
        public int Id { get; set; }

        // The display name of the food item (e.g., "Banana", "Grilled Chicken Breast").
        public string Name { get; set; } = null!;

        // Indicates the origin of the food data (e.g., 'User', 'Nutritionix', etc.).
        // Could later be extended to support toggling between different online sources.
        //public string Source { get; set; } = null!;

        // Could be used to categorize food items (e.g., Fruit, Vegetable, Protein).
        // Consider enabling this for more meaningful grouping or filtering in the UI.
        // public Category Category { get; set; }

        // Navigation property to associated nutritional data for this food item.
        public virtual NutritionData NutritionData { get; set; } = null!;

        // Navigation property for meal associations.
        // One-to-many: this food item can appear in multiple MealFoodItem join records.
        // Useful for tracking how and where the food was consumed.
        public virtual ICollection<MealFoodItem>? MealFoodItems { get; set; } = new HashSet<MealFoodItem>();
    }
}
