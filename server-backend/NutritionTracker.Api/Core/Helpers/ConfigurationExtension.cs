namespace NutritionTracker.Api.Core.Helpers
{
    public static class ConfigurationExtension
    {
        public static void AddEnvSettings(this IConfigurationBuilder configBuilder)
        {
            DotNetEnv.Env.Load();

            var jwtSecret = DotNetEnv.Env.GetString("JWT_SECRET")
                ?? throw new InvalidOperationException("JWT_SECRET not found in .env");

            var dbConnection = DotNetEnv.Env.GetString("DB_CONNECTION")
                ?? throw new InvalidOperationException("DB_CONNECTION not found in .env");

            var envConfig = new Dictionary<string, string>
            {
                { "Authentication:SecretKey", jwtSecret },
                { "ConnectionStrings:DefaultConnection", dbConnection }
            };

            configBuilder.AddInMemoryCollection(envConfig .Select(kvp => new KeyValuePair<string, string?>(kvp.Key, kvp.Value)));
        }
    }
}