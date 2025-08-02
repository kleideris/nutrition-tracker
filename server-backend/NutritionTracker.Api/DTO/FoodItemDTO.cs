using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.DTO
{
    public class FoodItemDTO
    {
        public string Name { get; set; } = null!;
        public virtual NutritionDataDTO NutritionData { get; set; } = null!;
    }
}
