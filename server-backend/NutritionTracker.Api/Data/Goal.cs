using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.Data
{
    public class Goal : BaseEntity
    {
        // Primary key that uniquely identifies each nutrition goal record.
        public int Id { get; set; }

        // Foreign key linking this goal to a specific user.
        public int UserId { get; set; }

        // Specifies the type of nutrition goal (e.g., Cutting, Maintenance, Bulking).
        // Used to categorize the user's intent and influence calorie/macro targets.
        public GoalType GoalType { get; set; }

        // Daily caloric intake target set (kcal).
        // Used to evaluate whether total consumption aligns with their goal.
        public double TargetCalories { get; set; }

        // Target amount of protein (grams) to consume per day.
        // Can be factored into macro balancing and meal planning.
        public double TargetProteinGrams { get; set; }

        // Daily carbohydrate target in grams.
        // Supports energy budgeting and nutrition profiling.
        public double TargetCarbsGrams { get; set; }

        // Desired fat intake per day in grams.
        // Completes the macro trio for comprehensive dietary tracking.
        public double TargetFatsGrams { get; set; }

        // Start date when this goal becomes active.
        // Useful for historical comparisons and period-based analytics.
        public DateOnly StartDate { get; set; }

        // Optional end date for the goal.
        // Allows for time-bounded tracking, phased nutrition plans, or ongoing goals when null.
        public DateOnly? EndDate { get; set; }

        // Navigation property to the user who owns this goal.
        // Enables querying goal history and personalization by user.
        public virtual User User { get; set; } = null!;
    }
}
