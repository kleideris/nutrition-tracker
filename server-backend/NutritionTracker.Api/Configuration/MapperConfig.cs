using AutoMapper;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Security;

namespace NutritionTracker.Api.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<UserProfileDto, UserProfile>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Ignores nulls to protect existing values.
            CreateMap<User, UserReadOnlyDto>().ReverseMap();
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => EncryptionUtil.EncryptPassword(src.Password!)));
            CreateMap<MealPostDto, Meal>().ReverseMap();
            CreateMap<Meal, MealReadOnlyDto>().ReverseMap();
            CreateMap<MealFoodItem, MealFoodItemDto>().ReverseMap();
            CreateMap<FoodItemDto, FoodItem>().ReverseMap();
            CreateMap<NutritionDataDto, NutritionData>().ReverseMap();
            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<User, UpdateUserDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


        }
    }
}
