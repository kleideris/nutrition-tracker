namespace NutritionTracker.Api.Services
{
    public interface IApplicationService
    {
        public AuthService AuthService { get; }
        public UserService UserService { get; }
        public MealService MealService { get; }
        public MealFoodItemService MealFoodItemService { get; }
        public FoodItemService FoodItemService { get; }
        public NutritionService NutritionService { get; }
    }
}
