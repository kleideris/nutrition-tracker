using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.Api.DTOs
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 50 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)(?=.*?\W)^.{8,}$", 
            ErrorMessage = "Password must contain at least one uppercase, one lowercase, one digit, and one special character")]
        public string? Password { get; set; }
    }
}
