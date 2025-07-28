using Microsoft.EntityFrameworkCore;

namespace NutritionTracker.Api.Data
{
    public static class ModelBuilderExtension
    {
        public static void AddBaseEntityAuditRules(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType));

            foreach (var entityType in entityTypes)
            {
                var builder = modelBuilder.Entity(entityType.ClrType);

                // Automatically sets the creation timestamp when the entity is first inserted using the database's current date/time.
                builder.Property("InsertedAt")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("GETDATE()");

                // Automatically sets the update timestamp on insert and every subsequent update using the database's current date/time.
                builder.Property("ModifiedAt")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("GETDATE()");
            }
        }
    }
}
