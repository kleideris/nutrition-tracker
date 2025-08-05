using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.DTO
{
    public class FoodItemDto
    {
        public string Name { get; set; } = null!;
        public virtual NutritionDataDto NutritionData { get; set; } = null!;
    }
}
