namespace NutritionTracker.Api.DTOs
{
    public class FoodNutritionDto
    {
        public string FoodName { get; set; }
        public double QuantityInGrams { get; set; }

        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Carbs { get; set; }
        public float Fat { get; set; }
    }
}
