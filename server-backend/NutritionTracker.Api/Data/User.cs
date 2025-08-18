using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.Data
{
    public class User : BaseEntity
    {
        /// <summary>
        /// Primary key that uniquely identifies each user in the system.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Login credentials and personal information.
        /// </summary>
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        /// <summary>
        /// Personal name details for display and identification.
        /// </summary>
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;

        /// <summary>
        /// Specifies the user's role within the system (e.g., RegularUser, Admin).
        /// </summary>
        public UserRole UserRole { get; set; }

        /// <summary>
        /// Navigation property linking to the user's detailed profile, which stores biometric, lifestyle, or personalization data.
        /// </summary>
        public virtual UserProfile? UserProfile { get; set; }

        /// <summary>
        /// Collection of meals associated with the user, representing their dietary log and daily intake history.
        /// </summary>
        public ICollection<Meal> Meals { get; set; } = [];

        /// <summary>
        /// Collection of nutrition or fitness goals.
        /// Each goal represents a distinct objective (e.g. weight loss, maintenance, bulking) and impacts calculated nutritional needs.
        /// </summary>
        public ICollection<Goal> Goals { get; set; } = [];
    }
}

