namespace NutritionTracker.Api.DTOs
{
    public class MealNutritionDto
    {
        public int MealId { get; set; }
        public List<FoodNutritionDto> Items { get; set; }

        public double TotalCalories { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalFat { get; set; }

    }
}
