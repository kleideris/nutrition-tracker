using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Repositories;
using Serilog;

namespace NutritionTracker.Api.Services
{
    public class MealFoodItemService(IUnitOfWork unitOfWork, IMapper mapper) : IMealFoodItemService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<MealFoodItemService> _logger = new LoggerFactory().AddSerilog().CreateLogger<MealFoodItemService>();



        /// work in progress
        public async Task<List<MealFoodItem>> CreateMealFoodItemsAsync(List<MealFoodItemDto> dtos)
        {
            var result = new List<MealFoodItem>();

            foreach (var dto in dtos)
            {
                // Optional: validate FoodItem exists
                var existingfoodItem = await _unitOfWork.FoodItemRepository.GetAsync(dto.FoodItemId) ??
                    throw new EntityNotFoundException("FoodItem", "Food item with id: " + dto.FoodItemId + " was not found");

                var item = new MealFoodItem
                {
                    FoodItemId = dto.FoodItemId,
                    Quantity = dto.Quantity,
                    UnitOfMeasurement = dto.UnitOfMeasurement
                };

                result.Add(item);
            }

            return result;
        }

        //WIP#
        public async Task<bool> AddFoodItemToMealAsync(int mealId, int foodItemId, float quantity, string unit)
        {
            var meal = _unitOfWork.MealRepository.GetAsync(mealId) ??
                throw new EntityNotFoundException("Meal", "Meal with id: " + mealId + " could not be found");

            var foodItem = _unitOfWork.FoodItemRepository.GetAsync(foodItemId) ??
                throw new EntityNotFoundException("FoodItem", "Food item with id: " + foodItemId + " could not be found");

            var existing = _unitOfWork.MealFoodItemRepository.ExistsAsync(mealId, foodItemId);
            if (existing != null) throw new EntityAlreadyExistsException("MealFoodItem", "MealFoodItem with mealId: " + mealId +
                " and  foodItemId: " + foodItemId + " already exists");
            
            var mealFoodItem = new MealFoodItem
            {
                MealId = mealId,
                FoodItemId = foodItemId,
                Quantity = quantity,
                UnitOfMeasurement = unit
            };

            await _unitOfWork.MealFoodItemRepository.AddAsync(mealFoodItem);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("MealFoodItem: {mealFoodItem} added successfully", mealFoodItem.Id);

            return true;
        }


        //WIP#
        public async Task<IEnumerable<MealFoodItem>> GetByMealIdAsync(int mealId) => 
            await _unitOfWork.MealFoodItemRepository.GetByMealIdAsync(mealId);


        //WIP #
        public async Task<bool> DeleteFoodItemOfMealAsync(int mealId, int foodItemId, float quantity, string unit)
        {
            var meal = _unitOfWork.MealRepository.GetAsync(mealId) ??
                throw new EntityNotFoundException("Meal", "Meal with id: " + mealId + " could not be found");

            var foodItem = _unitOfWork.FoodItemRepository.GetAsync(foodItemId) ??
                throw new EntityNotFoundException("FoodItem", "Food item with id: " + foodItemId + " could not be found");

            var mealFoodItem = _unitOfWork.MealFoodItemRepository.GetByJoinedIdsAsync(mealId, foodItemId) ??
                throw new EntityNotFoundException("MealFoodItem", "MealFoodItem with mealId: " + mealId +
                " and  foodItemId: " + foodItemId + " could not be found");

            await _unitOfWork.MealFoodItemRepository.DeleteAsync(mealFoodItem.Id);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("MealFoodItem: {mealFoodItem} added successfully", mealFoodItem.Id);

            return true;
        }


        //WIP#
        public async Task<MealFoodItem?> GetByJoinedIdsAsync(int mealId, int foodItemId) => 
            await _unitOfWork.MealFoodItemRepository.GetByJoinedIdsAsync(mealId, foodItemId);


        //WIP#
        public async Task<bool> UpdateQuantityOfFoodItemAsync(int mealId, int foodItemId, float quantity, string unit)
        {
            var meal = _unitOfWork.MealRepository.GetAsync(mealId) ??
               throw new EntityNotFoundException("Meal", "Meal with id: " + mealId + " could not be found");

            var foodItem = _unitOfWork.FoodItemRepository.GetAsync(foodItemId) ??
                throw new EntityNotFoundException("FoodItem", "Food item with id: " + foodItemId + " could not be found");

            var mealFoodItem = await _unitOfWork.MealFoodItemRepository.GetByJoinedIdsAsync(mealId, foodItemId) ??
                throw new EntityNotFoundException("MealFoodItem", "MealFoodItem with mealId: " + mealId +
                " and  foodItemId: " + foodItemId + " could not be found");

            await _unitOfWork.MealFoodItemRepository.UpdateQuantityAsync(mealId, foodItemId, quantity, unit);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("MealFoodItem's: {mealFoodItem} quantity updated successfully", mealFoodItem.Id);

            return true;
        }
    }
}
