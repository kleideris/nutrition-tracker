using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTOs
{
    public class UserReadOnlyDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public UserRole? UserRole { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
