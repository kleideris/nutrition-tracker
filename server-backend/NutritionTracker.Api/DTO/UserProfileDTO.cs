using NutritionTracker.Api.Core.Enums;

namespace NutritionTracker.Api.DTO
{
    public class UserProfileDto
    {
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public double HeightCm { get; set; }
        public double WeightKg { get; set; }
        public string? ActivityLvl { get; set; }

        // TODO: Add validations to properties
    }
}
