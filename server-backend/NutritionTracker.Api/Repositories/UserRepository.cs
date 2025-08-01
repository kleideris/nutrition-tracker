using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.Security;
using System.Linq.Expressions;

namespace NutritionTracker.Api.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDBContext context) : base(context)
        {
        }



        //Finished
        public async Task<User?> AuthenticateUserAsync(string usernameOrEmail, string password)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);

            if (user == null || !EncryptionUtil.IsValidPassword(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }


        //Finished
        // By using Expression<Func<User, bool>> Instead of Func<User, bool> we allows Entity Framework to parse and convert
        // the filters into SQL queries. Otherwise because it cant translate the func delegates
        // int sql queries, it would load everthing in memory and filter them there.
        public async Task<List<User>> GetAllUsersFilteredPaginatedAsync(int pageNumber, int pageSize,
            Expression<Func<User, bool>> predicates)
        {
            int skip = (pageNumber - 1) * pageSize;

            IQueryable<User> query = context.Users;

            query = query.Where(predicates ?? (u => true))  // fallback with (u => true), ensures no filters = full data
                 .Skip(skip)
                 .Take(pageSize);

            query = query.Skip(skip).Take(pageSize);

            return await query.ToListAsync();
        }

        
        //Finished
        public async Task<User?> GetUserByUsernameAsync(string username) => 
            await context.Users.FirstOrDefaultAsync(u => u.Username == username);


        //Finished
        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var existingUser = await context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            context.Users.Attach(user);
            context.Entry(user).State = EntityState.Modified;  // Marks the UserProfile entity as modified

            return existingUser;
        }
    }
}
