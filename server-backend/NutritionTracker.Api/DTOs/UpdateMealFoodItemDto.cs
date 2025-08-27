using NutritionTracker.Api.Data;
using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.Api.DTOs
{
    public class UpdateMealFoodItemDto
    {
        [Required]
        public float Quantity { get; set; }

        [Required]
        public string Unit { get; set; } = "grams";
    }
}
