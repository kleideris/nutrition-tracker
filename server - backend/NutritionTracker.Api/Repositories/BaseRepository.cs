
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

        // Adds a single entity to the database asynchronously
        public virtual async Task AddAsync(T entity) => await dbset.AddAsync(entity);

        // Adds a collection of entities to the database asynchronously
        public virtual async Task AddRangeAsync(IEnumerable<T> entity) => await dbset.AddRangeAsync(entity);

        // Retrieves an entity by its primary key ID asynchronously
        public virtual async Task<T?> GetAsync(int id) => await dbset.FindAsync(id);

        // Retrieves all entities of type T from the database asynchronously
        public virtual async Task<IEnumerable<T?>> GetAllAsync() => await dbset.ToListAsync();

        // Counts the total number of entities of type T in the database asynchronously
        public virtual async Task<int> GetCountAsync() => await dbset.CountAsync();

        // Marks an entity as modified in the context so changes can be saved later
        public virtual Task UpdateAsync(T entity)
        {
            dbset.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        // Deletes an entity by ID if it exists; returns true if removed from context
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
