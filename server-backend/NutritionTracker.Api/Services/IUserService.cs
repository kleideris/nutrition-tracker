using NutritionTracker.Api.Core.Filters;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTOs;

namespace NutritionTracker.Api.Services
{
    public interface IUserService
    {
        Task<User?> VerifyAndGetAsync(LoginUserDto credentials);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<List<User>> GetFilteredPaginatedAsync(int pageNumber, int pageSize, UserFiltersDTO userFilterDTO);
        Task<RegisterResult> RegisterAsync(RegisterUserDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
