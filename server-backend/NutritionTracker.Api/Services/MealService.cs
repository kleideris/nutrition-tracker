using AutoMapper;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTOs;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Repositories.Interfaces;
using NutritionTracker.Api.Services.Interfaces;
using Serilog;

namespace NutritionTracker.Api.Services
{
    public class MealService(IUnitOfWork unitOfWork, IMapper mapper) /*: IMealService*/
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<MealService> _logger = new LoggerFactory().AddSerilog().CreateLogger<MealService>();


        //also calls the mealfooditem repository internally to create the one to many connection
        public async Task<Meal?> AddAsync(MealType mealType, AddMealDto dto)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(dto.UserId) ?? 
                throw new EntityNotFoundException("User", "User with id: " + dto.UserId + " could not be found");

            var meal = new Meal
            {
                UserId = dto.UserId,
                MealType = mealType,
                Timestamp = dto.Timestamp,
                MealFoodItems = new List<MealFoodItem>()
            };


            foreach (var itemDto in dto.MealFoodItems)
            {
                var foodItem = await _unitOfWork.FoodItemRepository.GetAsync(itemDto.FoodItemId) ?? 
                    throw new EntityNotFoundException("FoodItem", "FoodItem with id: " + itemDto.FoodItemId + " could not be found");

                var mealFoodItem = new MealFoodItem
                {
                    FoodItem = foodItem,
                    Quantity = itemDto.Quantity,
                    UnitOfMeasurement = itemDto.UnitOfMeasurement,
                    Meal = meal
                };

                await _unitOfWork.MealFoodItemRepository.AddAsync(mealFoodItem);
            }

            await _unitOfWork.MealRepository.AddAsync(meal);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Meal Registered: {MealId}", meal.Id);

            return meal;
        }


        public async Task<Meal?> GetByIdAsync(int mealId) => await _unitOfWork.MealRepository.GetAsync(mealId);


        public Task<IEnumerable<Meal>> GetByUserAsync(int userId) => _unitOfWork.MealRepository.GetByUserAsync(userId);
    }
}



