namespace NutritionTracker.Api.Repositories
{
    // Defines a contract for working with multiple repositories in a single transactional scope.
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        MealRepository MealRepository { get; }

        Task<bool> SaveAsync();
    }
}
