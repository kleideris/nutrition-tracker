using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;

namespace NutritionTracker.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> AuthenticateAsync(string username, int password);
        Task<User?> UpdateUserAsync(int userId, User user);  // TODO: think if i should change User with a UserDTO)
        Task<User?> UpdateUserProfileAsync(int userId, UserProfileDTO dto);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersFilteredPaginatedAsync(int pageNumber, int pageSize,
            List<Func<User, bool>> predicates);
        // Task<bool> IsEmailTakenAsync(string email);
    }
}
