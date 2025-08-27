using Microsoft.Data.SqlClient;
using NutritionTracker.Api.Data;

public static class AdminSeeder
{
    public static void Seed(IServiceProvider services)
    {
        try
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
                Console.WriteLine("<|> Admin user seeded successfully.");
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("<|> Admin user already exists. Skipping seeding.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("<|> Could not connect to the database.");
            Console.WriteLine($"Details: {ex.Message}");
            //TODO: log to a file or monitoring system
        }
        catch (Exception ex)
        {
            Console.WriteLine("<|> An unexpected error occurred.");
            Console.WriteLine($"Details: {ex.Message}");

        }
    }
}