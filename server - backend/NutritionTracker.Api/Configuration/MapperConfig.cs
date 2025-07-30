using AutoMapper;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;

namespace NutritionTracker.Api.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserProfile, UserProfileDTO>();
            CreateMap<UserProfileDTO, UserProfile>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Ignores nulls to protect existing values.

        }
    }
}
