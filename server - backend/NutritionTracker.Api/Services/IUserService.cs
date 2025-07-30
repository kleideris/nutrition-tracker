using NutritionTracker.Api.Core.Filters;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;

namespace NutritionTracker.Api.Services
{
    public interface IUserService
    {
        Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersFilteredPaginatedAsync(int pageNumber, int pageSize, UserFiltersDTO userFilterDTO);

        //string CreateUserToken(int userId, string username, string email, UserRole userRole, string appSecurityKey);
    }
}
