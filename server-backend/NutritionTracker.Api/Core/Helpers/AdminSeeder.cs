using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.Models;

namespace NutritionTracker.Api.Core.Helpers
{
    public class AdminSeeder
    {
        //public static async Task SeedInitialAdminAsync(IServiceProvider serviceProvider)
        //{
        //    using var scope = serviceProvider.CreateScope();

        //    var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
        //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        //    var adminEmail = config["InitialAdmin:Email"];
        //    var adminPassword = config["InitialAdmin:Password"];

        //    var adminExists = await userManager.Users.AnyAsync(u => u.Email == adminEmail);

        //    if (!adminExists)
        //    {
        //        var adminUser = new ApplicationUser
        //        {
        //            Username = adminEmail,
        //            Email = adminEmail
        //        };

        //        var result = await userManager.CreateAsync(adminUser, adminPassword!);
        //        if (result.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(adminUser, "Admin");
        //            Console.WriteLine("✅ Initial admin created.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("❌ Failed to create initial admin:");
        //            foreach (var error in result.Errors)
        //                Console.WriteLine($"- {error.Description}");
        //        }
        //    }
        //}
    }
}
