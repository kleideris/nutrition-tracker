using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.Repositories.Interfaces;

namespace NutritionTracker.Api.Repositories
{
    public abstract class BaseRepository<T>(AppDBContext context) : IBaseRepository<T> where T : class
    {
        protected readonly AppDBContext context = context;  // The EF database context injected via constructor
        protected readonly DbSet<T> dbset = context.Set<T>();  // Generic DbSet for accessing entities of type T


        public virtual async Task AddAsync(T entity) => await dbset.AddAsync(entity);


        public virtual async Task AddRangeAsync(IEnumerable<T> entity) => await dbset.AddRangeAsync(entity);


        public virtual async Task<T?> GetAsync(int id) => await dbset.FindAsync(id);


        public virtual async Task<IEnumerable<T?>> GetAllAsync() => await dbset.ToListAsync();


        public virtual async Task<int> GetCountAsync() => await dbset.CountAsync();


        public virtual Task UpdateAsync(T entity)
        {
            dbset.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }


        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? existingEntity = await GetAsync(id);
            if (existingEntity is null) return false;
            dbset.Remove(existingEntity);
            return true;
        }
    }
}
