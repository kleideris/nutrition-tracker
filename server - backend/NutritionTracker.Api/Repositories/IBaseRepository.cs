namespace NutritionTracker.Api.Repositories
{
    public interface IBaseRepository<T>
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);

        Task<T?> GetAsync(int id);
        Task<IEnumerable<T?>> GetAllAsync();
        Task<int> GetCountAsync();

        Task UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);
    }
}
