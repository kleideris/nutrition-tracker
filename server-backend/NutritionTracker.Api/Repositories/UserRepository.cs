using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Core.Security;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.Repositories.Interfaces;
using System.Linq.Expressions;

namespace NutritionTracker.Api.Repositories
{
    /// <summary>
    /// Repository for performing data operations related to <see cref="User"/> entities.
    /// </summary>
    /// 
    public class UserRepository(AppDBContext context) : BaseRepository<User>(context) , IUserRepository
    {

        /// <summary>
        /// Authenticates a user by verifying their username/email and password.
        /// </summary>
        /// <param name="usernameOrEmail">The username or email of the user.</param>
        /// <param name="password">The plaintext password to validate.</param>
        /// <returns>
        /// A <see cref="User"/> object if authentication is successful; otherwise, <c>null</c>.
        /// </returns>
        /// 
        public async Task<User?> AuthenticateAsync(string usernameOrEmail, string password)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);

            if (user == null || !EncryptionUtil.IsValidPassword(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }


        /// <summary>
        /// Retrieves a paginated list of users filtered by the specified predicate.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (1-based).</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <param name="predicates">An expression used to filter users.</param>
        /// <returns>A list of <see cref="User"/> objects matching the filter criteria.</returns>
        ///
        public async Task<List<User>> GetAllFilteredPaginatedAsync(int pageNumber, int pageSize,
            Expression<Func<User, bool>> predicates)  // Uses Expression<Func<User, bool>> to ensure filtering is performed in SQL, avoiding in-memory filtering.
        {
            int skip = (pageNumber - 1) * pageSize;

            IQueryable<User> query = context.Users;

            query = query.Where(predicates ?? (u => true))  // fallback with (u => true), ensures no filters = full data
                .OrderBy(u => u.Id)
                .Skip(skip)
                .Take(pageSize);

            query = query.Skip(skip).Take(pageSize);

            return await query.ToListAsync();
        }


        /// <summary>
        /// Updates an existing user with new data.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated <see cref="User"/> object.</param>
        /// <returns>
        /// The original <see cref="User"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        /// 
        public async Task<User?> UpdateAsync(int id, User user)
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


        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>A <see cref="User"/> object if found; otherwise, <c>null</c>.</returns>
        public async Task<User?> GetByUsernameAsync(string username) => await context.Users.FirstOrDefaultAsync(u => u.Username == username);


        /// <summary>
        /// Checks whether a user with the specified email already exists.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns><c>true</c> if the email exists; otherwise, <c>false</c>.</returns>
        /// 
        public async Task<bool> EmailExistsAsync(string? email) => await context.Users.AnyAsync(u => u.Email == email);


        /// <summary>
        /// Asynchronously counts the number of users with the specified role.
        /// </summary>
        /// <param name="role">The <see cref="UserRole"/> to filter users by.</param>
        /// <returns>An <see cref="int"/> representing the total number of users who match the given role.</returns>
        /// 
        public async Task<int> GetCountByRoleAsync(UserRole role) => await context.Users.CountAsync(u => u.UserRole == role);
    }
}
