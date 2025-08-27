using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.Api.DTOs
{
    public class AddMealFoodItemDto
    {
        [Required(ErrorMessage = "Quantity is required.")]
        public float Quantity { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        public string Unit { get; set; } = "grams";
    }
}
