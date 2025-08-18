namespace NutritionTracker.Api.Data
{
    public class BaseEntity
    {
        /// <summary>
        /// Timestamp marking when the entity was created (useful for audit logging and lifecycle tracking).
        /// </summary>
        public DateTime InsertedAt { get; set; }

        /// <summary>
        /// Nullable timestamp for when the entity was last updated; null if no updates have occurred.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}

