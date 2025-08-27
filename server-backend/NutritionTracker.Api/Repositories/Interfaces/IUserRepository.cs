using NutritionTracker.Api.Data;
using System.Linq.Expressions;

namespace NutritionTracker.Api.Repositories.Interfaces
{
    /// <summary>
    /// Defines data access operations for managing User entities,
    /// including authentication, retrieval, updates, filtering, and existence checks.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Authenticates a user based on provided username and password credentials. Returns the matching User entity if authentication is successful; otherwise, null.
        /// </summary>
        Task<User?> AuthenticateAsync(string username, string password);

        /// <summary>
        /// Updates the details of an existing User entity identified by its ID. Returns the updated User entity if successful; otherwise, null.
        /// </summary>
        Task<User?> UpdateAsync(int id, User user);

        /// <summary>
        /// Retrieves a User entity based on the specified username. Returns null if no matching user is found.
        /// </summary>
        Task<User?> GetByUsernameAsync(string username);

        /// <summary>
        /// Retrieves a paginated and filtered list of User entities based on the provided criteria.
        /// </summary>
        Task<List<User>> GetAllFilteredPaginatedAsync(int pageNumber, int pageSize, Expression<Func<User, bool>> predicates);

        /// <summary>
        /// Checks whether a User entity exists with the specified email address. Returns true if the email is already registered; otherwise, false.
        /// </summary>
        Task<bool> EmailExistsAsync(string email);
    }
}
