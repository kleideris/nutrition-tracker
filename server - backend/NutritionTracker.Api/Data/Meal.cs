using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.Data
{
    public class Meal : BaseEntity
    {
        // Primary key used to uniquely identify each meal record.
        public int Id { get; set; }

        // Foreign key linking this meal to a specific user.
        public int UserId { get; set; }

        // Optional name given by users to personalize meals (e.g., "My Salad").
        // Useful for bookmarking or favoriting recurring meal setups.
        // public string? MealName { get; set; }

        // Categorizes the type of meal (e.g., Breakfast, Snack, Dinner).
        // Enables grouped analytics and filtering (e.g., analyze all snacks).
        public MealType MealType { get; set; }

        // Timestamp marking when the meal was consumed.
        // Enables time-based statistics, trends, and historical tracking.
        public DateTime Timestamp { get; set; }

        // Navigation property: each meal is linked to one user.
        // Many-to-one relationship — a user can have multiple meals.
        public virtual User? User { get; set; }

        // Navigation property: links this meal to its food items via join records.
        // One-to-many relationship — a single meal can include many MealFoodItem entries.
        public virtual ICollection<MealFoodItem> MealFoodItems { get; set; } = new HashSet<MealFoodItem>();
    }
}
