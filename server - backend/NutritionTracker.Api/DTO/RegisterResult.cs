namespace NutritionTracker.Api.DTO
{
    public class RegisterResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int UserId { get; set; }
    }
}
