namespace NutritionTracker.Api.DTO
{
    public class NutritionDataDto
    {
        public float Calories { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        public float Protein { get; set; }
        public int FoodItemId { get; set; }
        public double ServingSizeGrams { get; set; }
    }
}
