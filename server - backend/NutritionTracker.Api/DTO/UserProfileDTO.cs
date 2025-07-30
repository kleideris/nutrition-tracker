using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTO
{
    public class UserProfileDTO
    {
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public double HeightCm { get; set; }
        public double WeightKg { get; set; }
        public string? ActivityLvl { get; set; }
    }
}
