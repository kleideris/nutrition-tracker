namespace NutritionTracker.Api.Data
{
    public class FoodItem : BaseEntity
    {
        /// <summary>
        /// Primary key used to uniquely identify each food item in the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The display name of the food item (e.g., "Banana", "Grilled Chicken Breast").
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Navigation property to associated nutritional data for this food item.
        /// </summary>
        public virtual NutritionData NutritionData { get; set; } = null!;

        /// <summary>
        /// Navigation property for meal associations.
        /// One-to-many: this food item can appear in multiple MealFoodItem join records.
        /// </summary>
        public virtual ICollection<MealFoodItem>? MealFoodItems { get; set; } = new HashSet<MealFoodItem>();
    }
}
