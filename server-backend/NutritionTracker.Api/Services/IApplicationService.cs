namespace NutritionTracker.Api.Services
{
    public interface IApplicationService
    {
        public UserService UserService { get; }
        public MealService MealService { get; }
    }
}
