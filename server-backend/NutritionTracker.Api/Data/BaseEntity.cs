namespace NutritionTracker.Api.Data
{
    public class BaseEntity
    {
        // Timestamp marking when the entity was created (useful for audit logging and lifecycle tracking).
        public DateTime InsertedAt { get; set; }

        // Nullable timestamp for when the entity was last updated; null if no updates have occurred.
        public DateTime? UpdatedAt { get; set; }
    }
}

