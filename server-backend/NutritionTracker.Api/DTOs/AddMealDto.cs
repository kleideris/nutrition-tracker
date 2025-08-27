using NutritionTracker.Api.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.Api.DTOs
{
    public class AddMealDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Timestamp is required.")]
        public DateTime Timestamp { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one food item is required.")]
        public List<MealFoodItemDto> MealFoodItems { get; set; } = [];

    }
}
