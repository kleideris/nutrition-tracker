namespace NutritionTracker.Api.Data
{
    public class NutritionData
    {
        // Primary key that uniquely identifies this nutrition profile entry.
        public int Id { get; set; }

        // Energy content in kilocalories.
        public float Calories { get; set; }

        // Total fat content, typically in grams.
        public float Fats { get; set; }

        // Total carbohydrate content, typically in grams.
        public float Carbohydrates { get; set; }

        // Total protein content, typically in grams.
        public float Protein { get; set; }

        // Foreign key linking this nutrition data to its corresponding food item.
        // One-to-one relationship — each FoodItem has a single NutritionData entry.
        public int FoodItemId { get; set; }

        // Indicates the weight in grams that defines one standard serving size for this food item.
        // Used to scale nutrient values from a 100g base to real-world portions (e.g., 1 slice, 1 jar, 1 banana).
        public double ServingSizeGrams { get; set; }

        // Navigation property for accessing the related food item from its nutritional profile.
        public virtual FoodItem FoodItem { get; set; } = null!;
    }
}
 