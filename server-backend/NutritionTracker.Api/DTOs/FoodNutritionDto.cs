namespace NutritionTracker.Api.DTOs
{
    public class FoodNutritionDto
    {
        public string FoodName { get; set; }
        public double QuantityInGrams { get; set; }

        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }
    }
}
