using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.Data
{
    public class Meal : BaseEntity
    {
        /// <summary>
        /// Primary key used to uniquely identify each meal record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key linking this meal to a specific user.
        /// </summary>        
        public int UserId { get; set; }

        /// <summary>
        /// Categorizes the type of meal(e.g., Breakfast, Snack, Dinner).
        /// Enables grouped analytics and filtering(e.g., analyze all snacks).
        /// </summary>
        public MealType MealType { get; set; }

        /// <summary>
        /// Timestamp marking when the meal was consumed.
        /// Enables time-based statistics, trends, and historical tracking.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Navigation property: each meal is linked to one user.
        /// Many-to-one relationship — a user can have multiple meals.
        /// </summary>   
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Navigation property: links this meal to its food items via join records.
        /// One-to-many relationship — a single meal can include many MealFoodItem entries.
        /// </summary>
        public virtual ICollection<MealFoodItem> MealFoodItems { get; set; } = new HashSet<MealFoodItem>();
    }
}
