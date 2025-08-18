using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Data;
using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.Api.DTOs
{
    public class MealDto
    {
        public int UserId { get; set; }
        public MealType MealType { get; set; }
        public DateTime TimeStamp { get; set; }

        [Required(ErrorMessage = "Meal must contain at least one food item")]
        public List<MealFoodItem> MealFoodItems { get; set; } = [];
    }
}
