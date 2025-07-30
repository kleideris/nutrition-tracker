namespace NutritionTracker.Api.Repositories
{
    public static class RepositoriesDIExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
