using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.Services
{
    public interface IAuthService
    {
        string CreateUserToken(int userId, string username, string email, UserRole userRole, string appSecurityKey);
    }
}
