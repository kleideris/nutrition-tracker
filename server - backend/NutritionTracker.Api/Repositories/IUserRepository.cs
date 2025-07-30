using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using System.Linq.Expressions;

namespace NutritionTracker.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User?> UpdateUserAsync(int id, User user);  // TODO: think if i should change User with a UserDTO)
        //Task<User?> UpdateUserProfileAsync(int id, UserProfileDTO dto);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersFilteredPaginatedAsync(int pageNumber, int pageSize,
            Expression<Func<User, bool>> predicates);
        // Task<bool> IsEmailTakenAsync(string email);
    }
}
