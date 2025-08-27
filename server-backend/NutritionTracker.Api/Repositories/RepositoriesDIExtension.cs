using NutritionTracker.Api.Repositories.Interfaces;

namespace NutritionTracker.Api.Repositories
{
    /// <summary>
    /// Provides extension methods for registering repository-related services into the application's dependency injection container.
    /// </summary>
    public static class RepositoriesDIExtension
    {
        #region docs
        /// <summary>
        /// Registers repository interfaces and their implementations with scoped lifetimes.
        /// Specifically, adds <see cref="IUnitOfWork"/> mapped to <see cref="UnitOfWork"/>.
        /// </summary>
        /// <param name="services">The service collection to which the repositories are added.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> with repository services registered.</returns>
        #endregion
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
