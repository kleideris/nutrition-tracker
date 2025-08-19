using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.DTOs;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    [Route("api/meals")]
    [ApiController]
    public class MealController(IApplicationService applicationService, IConfiguration configuration,
        IMapper mapper) : BaseController(applicationService)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;


        [HttpPost("meal-type/{mealType}")]
        [Authorize]
        public async Task<IActionResult> Add(MealType mealType, [FromBody] AddMealDto dto)
        {
            var existingMeals = await _applicationService.MealService.GetByUserAsync(dto.UserId);

            var createdMeal = await _applicationService.MealService.AddAsync(mealType, dto);
            if (createdMeal == null)
                return BadRequest(new { message = "Failed to log meal." });

            var mealDto = _mapper.Map<MealReadOnlyDto>(createdMeal);

            // Adds aggregated nutrition manually
            var aggregated = _applicationService.NutritionService.CalculateNutrition(createdMeal);
            mealDto.TotalCalories = aggregated.TotalCalories;
            mealDto.TotalProtein = aggregated.TotalProtein;
            mealDto.TotalCarbs = aggregated.TotalCarbs;
            mealDto.TotalFats = aggregated.TotalFats;

            return Ok(new { message = "Meal logged successfully", meal = mealDto });
        }


        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var meal = await _applicationService.MealService.GetByIdAsync(id) ?? throw new EntityNotFoundException("Meal", "Meal: " + id + " NotFound");
            var returnedDto = _mapper.Map<MealReadOnlyDto>(meal);
            return Ok(returnedDto);
        }


        //[HttpGet("user-id/{userId}")]
        //public async Task<IActionResult> GetByUser(int userId)
        //{
        //    var meals = await _applicationService.MealService.GetByUserAsync(userId) ?? throw new EntityNotFoundException("Meal", "Meal: " + userId + " NotFound");
        //    return Ok(meals);
        //}

        [HttpGet("user-id/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetByUserAsync(int userId)
        {
            var meals = await _applicationService.MealService.GetByUserAsync(userId);

            var result = new List<MealReadOnlyDto>();

            foreach (var meal in meals)
            {
                if (meal.MealFoodItems == null || !meal.MealFoodItems.Any())
                    continue;

                var nutrition = _applicationService.NutritionService.CalculateNutrition(meal);

                var dto = new MealReadOnlyDto
                {
                    Id = meal.Id,
                    MealType = meal.MealType,
                    Timestamp = meal.Timestamp,
                    TotalCalories = nutrition.TotalCalories,
                    TotalProtein = nutrition.TotalProtein,
                    TotalCarbs = nutrition.TotalCarbs,
                    TotalFats = nutrition.TotalFats,
                    FoodItems = meal.MealFoodItems.Select(mfi => new MealFoodItemDto
                    {
                        FoodItemId = mfi.FoodItemId,
                        Quantity = mfi.Quantity,
                        UnitOfMeasurement = mfi.UnitOfMeasurement,
                        FoodName = mfi.FoodItem?.Name,
                        Calories = mfi.FoodItem?.NutritionData?.Calories,
                        Protein = mfi.FoodItem?.NutritionData?.Protein,
                        Carbs = mfi.FoodItem?.NutritionData?.Carbohydrates,
                        Fats = mfi.FoodItem?.NutritionData?.Fats
                    }).ToList()
                };

                result.Add(dto);
            }

            return Ok(result);
        }
    }
}