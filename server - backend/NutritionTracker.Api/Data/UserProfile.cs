using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.Data
{
    public class UserProfile : BaseEntity
    {
        // Primary key to uniquely identify each user profile.
        public int Id { get; set; }

        // User's birth date, useful for age-based nutrition logic.
        public DateOnly DateOfBirth { get; set; }

        // Biological gender — used for metabolic modeling or nutrition predictions.
        public Gender Gender { get; set; }

        // Height in centimeters (cm).
        public double HeightCm { get; set; }

        // Weight in kilograms (kg).
        public double WeightKg { get; set; }

        // Optional activity level descriptor (e.g., Sedentary, Active, Athlete).
        // Can be used in TDEE/BMR calculations to tailor daily intake goals.
        public string? ActivityLvl { get; set; }

        // Foreign key linking this user profile to a specific user.
        public int UserId { get; set; }

        // Navigation property representing the associated user entity
        public virtual User? User { get; set; }
    }
}
