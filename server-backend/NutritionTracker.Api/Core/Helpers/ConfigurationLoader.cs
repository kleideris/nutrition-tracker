using Microsoft.AspNetCore.Components.Sections;

namespace NutritionTracker.Api.Core.Helpers
{
    public class ConfigurationLoader
    {
        public static void LoadEnvironmentVariables(IConfiguration configuration)
        {
            try 
            {
                var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
                if (string.IsNullOrEmpty(jwtSecret))
                    throw new InvalidOperationException("JWT_SECRET environment variable is not set.");

                configuration["Authentication:SecretKey"] = jwtSecret;

                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("DB_CONNECTION environment variable is not set or empty.");

                configuration["ConnectionStrings:DefaultConnection"] = connectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
