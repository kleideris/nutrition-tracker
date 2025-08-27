namespace NutritionTracker.Api.DTOs
{
    public class MealNutritionDto
    {
        public int MealId { get; set; }
        public List<FoodNutritionDto>? Items { get; set; }

        public float TotalCalories { get; set; }
        public float TotalProtein { get; set; }
        public float TotalCarbs { get; set; }
        public float TotalFats { get; set; }

    }
}
