using NutritionTracker.Api.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.Api.DTOs
{
    public class UpdateUserRoleDto
    {
        [Required(ErrorMessage = "New role is required.")]
        [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid role type.")]
        public UserRole NewRole { get; set; }
    }
}
