namespace NutritionTracker.Api.Repositories
{
    public interface IBaseRepository<T>
    {
        // Asynchronously adds a single entity to the database
        Task AddAsync(T entity);

        // Asynchronously adds multiple entities to the database in one batch
        Task AddRangeAsync(IEnumerable<T> entity);

        // Retrieves a single entity by primary key ID
        Task<T?> GetAsync(int id);

        // Retrieves all entities of type T from the database
        Task<IEnumerable<T?>> GetAllAsync();

        // Returns the total count of entities of type T
        Task<int> GetCountAsync();

        // Marks an entity as updated in the database (does not immediately save)
        Task UpdateAsync(T entity);

        // Removes an entity by ID; returns true if successfully marked for deletion
        Task<bool> DeleteAsync(int id);
    }
}
