using NutritionTracker.Api.Data;
using System.Linq.Expressions;

namespace NutritionTracker.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> AuthenticateAsync(string username, string password);

        Task<User?> UpdateAsync(int id, User user);

        Task<User?> GetByUsernameAsync(string username);

        Task<List<User>> GetAllFilteredPaginatedAsync(int pageNumber, int pageSize, Expression<Func<User, bool>> predicates);

         Task<bool> EmailExistsAsync(string email);
    }
}
