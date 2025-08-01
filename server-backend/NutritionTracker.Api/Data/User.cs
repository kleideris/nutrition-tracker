using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.Data
{
    public class User : BaseEntity
    {
        // Primary key that uniquely identifies each user in the system.
        public int Id { get; set; }

        // Login credentials and personal information.
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        // Personal name details for display and identification.
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;

        // Specifies the user's role within the system (e.g., RegularUser, Admin).
        public UserRole UserRole { get; set; }

        // Navigation property linking to the user's detailed profile, which stores biometric, lifestyle, or personalization data.
        public virtual UserProfile? UserProfile { get; set; }

        // Collection of meals associated with the user, representing their dietary log and daily intake history.
        public ICollection<Meal> Meals { get; set; } = [];

        // Collection of nutrition or fitness goals.
        // Each goal represents a distinct objective (e.g. weight loss, maintenance, bulking) and impacts calculated nutritional needs.
        public ICollection<Goal> Goals { get; set; } = [];
    }
}

