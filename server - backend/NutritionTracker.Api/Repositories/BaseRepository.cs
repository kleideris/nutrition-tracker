
using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;

namespace NutritionTracker.Api.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDBContext context;  // The EF database context injected via constructor
        protected readonly DbSet<T> dbset;  // Generic DbSet for accessing entities of type T

        // Constructor: Injects the AppDBContext and initializes the DbSet<T>
        public BaseRepository(AppDBContext context)
        {
            this.context = context;
            dbset = context.Set<T>();  // dynamically retrieves Dbset (of the entity thats refferenced)
        }

        
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

        // NOTE: Does NOT save changes to the database—requires SaveChangesAsync externally
        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? existingEntity = await GetAsync(id);
            if (existingEntity is null) return false;
            dbset.Remove(existingEntity);
            return true;

            // TODO: Check in the end if this return logic is preffered:
            // return await context.SaveChangesAsync() > 0;
        }
    }
}
