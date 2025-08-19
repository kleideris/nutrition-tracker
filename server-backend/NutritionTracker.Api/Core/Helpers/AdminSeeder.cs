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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                Firstname = "ad",
                Lastname = "min",
                UserRole = NutritionTracker.Api.Core.Enums.UserRole.Admin
            };

            db.Users.Add(adminUser);
            db.SaveChanges();
        }
    }
}