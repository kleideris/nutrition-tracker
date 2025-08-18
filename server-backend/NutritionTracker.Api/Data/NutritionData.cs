namespace NutritionTracker.Api.Data
{
    public class NutritionData
    {
        /// <summary>
        /// Primary key that uniquely identifies this nutrition profile entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Energy content in kilocalories.
        /// </summary>
        public float Calories { get; set; }

        /// <summary>
        /// Total fat content, typically in grams.
        /// </summary>
        public float Fats { get; set; }

        /// <summary>
        /// Total carbohydrate content, typically in grams.
        /// </summary>
        public float Carbohydrates { get; set; }

        /// <summary>
        /// Total protein content, typically in grams.
        /// </summary>
        public float Protein { get; set; }

        /// <summary>
        /// Foreign key linking this nutrition data to its corresponding food item.
        /// One-to-one relationship — each FoodItem has a single NutritionData entry.
        /// </summary>
        public int FoodItemId { get; set; }

        /// <summary>
        /// Indicates the weight in grams that defines one standard serving size for this food item.
        /// Used to scale nutrient values from a 100g base to real-world portions (e.g., 1 slice, 1 jar, 1 banana).
        /// </summary>
        //public double ServingSizeGrams { get; set; }

        /// <summary>
        /// Navigation property for accessing the related food item from its nutritional profile.
        /// </summary>
        public virtual FoodItem FoodItem { get; set; } = null!;
    }
}
 