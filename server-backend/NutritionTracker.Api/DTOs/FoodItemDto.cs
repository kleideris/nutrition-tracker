using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.DTOs
{
    public class FoodItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual NutritionDataDto NutritionData { get; set; } = null!;
    }
}
