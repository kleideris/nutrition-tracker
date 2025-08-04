using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.Api.DTO
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 50 characters.")]
        public string? UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)(?=.*?\W)^.{8,}$", 
            ErrorMessage = "Password must contain at least one uppercase, one lowercase, one digit, and one special character")]
        public string? Password { get; set; }

        // TODO: public bool KeepLoggedIn { get; set; }
        //TODO: Make the dto accept an email too instead of a username
    }
}
