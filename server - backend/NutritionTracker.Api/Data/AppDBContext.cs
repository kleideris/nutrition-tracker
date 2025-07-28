using Microsoft.EntityFrameworkCore;

namespace NutritionTracker.Api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        // DbSets represent the database tables
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Meal> Meals { get; set; } = null!;
        public DbSet<MealFoodItem> MealFoodItems { get; set; } = null!;
        public DbSet<FoodItem> FoodItems { get; set; } = null!;
        public DbSet<NutritionData> NutritionData { get; set; } = null!;
        public DbSet<Goal> Goals { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply audit defaults (InsertedAt, ModifiedAt) for entities inheriting BaseEntity using extension class.
            modelBuilder.AddBaseEntityAuditRules();  // <---------------------------- TODO: Understand how this works and is called exactly!!

            modelBuilder.Entity<User>(entity =>
            {
                // Converts UserRole enum to a string in the db.
                entity.Property(e => e.UserRole).HasConversion<string>();

                entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();
                entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();

                // Configure one-to-one: User → UserProfile.
                entity.HasOne(u => u.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                // Configure one-to-many: User → Goal.
                entity.HasMany(u => u.Goals)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                // Configure one-to-many: User → Meal.
                entity.HasMany(u => u.Meals)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                // Converts MealTime enum to a string in the db.
                entity.Property(e => e.MealType).HasConversion<string>();

                // Configure many-to-one: Meal → User.
                entity.HasOne(m => m.User)
                .WithMany(u => u.Meals)
                .HasForeignKey(m => m.UserId);

                // Configure one-to-many: Meal → MealFoodItem.
                entity.HasMany(m => m.MealFoodItems)
                .WithOne(mfi => mfi.Meal)
                .HasForeignKey(mfi => mfi.MealId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MealFoodItem>(entity =>
            {
                // Configure many-to-one: MealFoodItem → Meal
                entity.HasOne(mfi => mfi.Meal)
                .WithMany(m => m.MealFoodItems)
                .HasForeignKey(mfi => mfi.MealId);

                // Configure many-to-one: MealFoodItem → FoodItem
                entity.HasOne(mfi => mfi.FoodItem)
               .WithMany(f => f.MealFoodItems)
               .HasForeignKey(mfi => mfi.FoodItemId);
            });

            modelBuilder.Entity<FoodItem>(entity =>
            {
                // Configure one-to-many: FoodItem → MealFoodItem.
                entity.HasMany(f => f.MealFoodItems)
                .WithOne(mfi => mfi.FoodItem)
                .HasForeignKey(mfi => mfi.FoodItemId);

                // Configure one-to-many: FoodItem → MealFoodItem.
                entity.HasMany(m => m.MealFoodItems)
                .WithOne(mfi => mfi.FoodItem)
                .HasForeignKey(mfi => mfi.FoodItemId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents deleting shared FoodItem when a meal is deleted!
            });

            modelBuilder.Entity<NutritionData>(entity =>
            {
                // Configure one-to-one: NutritionData → FoodItem.
                entity.HasOne(n => n.FoodItem)
                .WithOne(f => f.NutritionData)
                .HasForeignKey<NutritionData>(n => n.FoodItemId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                // Configure many-to-one: Goal → User
                entity.HasOne(g => g.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserId);
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                // Converts Gender enum to a string in the db.
                entity.Property(e => e.Gender).HasConversion<string>();

                // Configure one-to-one: UserProfile → User
                entity.HasOne(p => p.User)
                .WithOne(u => u.UserProfile)
                .HasForeignKey<UserProfile>(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });


            // TODO: Add Indexes To every Entity that needs them.
        }
    }
}



            