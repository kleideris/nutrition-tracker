using NutritionTracker.Api.Data;

public static class AdminSeeder
{
    public static void Seed(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();

        if (!db.Users.Any(u => u.UserRole == NutritionTracker.Api.Core.Enums.UserRole.Admin))
        {
            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@email.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin.94"),
                //PasswordHash = "Admin.94",
                Firstname = "ad",
                Lastname = "min",
                UserRole = NutritionTracker.Api.Core.Enums.UserRole.Admin
            };

            db.Users.Add(adminUser);
            Console.WriteLine("👤 Admin user seeded successfully.");
            db.SaveChanges();
        }
        else
        {
            Console.WriteLine("ℹ️ Admin user already exists. Skipping seeding.");
        }
    }
}