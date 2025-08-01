namespace NutritionTracker.Api.Repositories
{
    // Defines a contract for working with multiple repositories in a single transactional scope.
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }

        Task<bool> SaveAsync();
    }
}
