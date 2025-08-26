using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.DTOs;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    /// <summary>
    /// Controller responsible for managing meal-related operations such as logging, retrieving,
    /// updating, and deleting meals. Each meal can include multiple food items and is associated
    /// with a specific user and meal type.
    /// </summary>
    /// 
    [Route("api/meals")]
    [ApiController]
    public class MealController(IApplicationService applicationService, IConfiguration configuration,
        IMapper mapper) : BaseController(applicationService)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;


        /// <summary>
        /// Retrieves a specific meal by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the meal to retrieve.</param>
        /// <returns>
        /// Returns <see cref="OkObjectResult"/> with the meal details if found.
        /// Throws <see cref="EntityNotFoundException"/> if the meal does not exist.
        /// </returns>
        /// 
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var meal = await _applicationService.MealService.GetByIdAsync(id) ?? throw new EntityNotFoundException("Meal", "Meal: " + id + " NotFound");
            var returnedDto = _mapper.Map<MealReadOnlyDto>(meal);
            return Ok(returnedDto);
        }


        /// <summary>
        /// Retrieves all meals logged by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose meals are being retrieved.</param>
        /// <returns>
        /// Returns <see cref="OkObjectResult"/> with a list of meals, each including aggregated nutrition data.
        /// Meals without food items are excluded from the result.
        /// </returns>
        /// 
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


        /// <summary>
        /// Logs a new meal for a user with the specified meal type and food items.
        /// </summary>
        /// <param name="mealType">The type of meal being logged (e.g., Breakfast, Lunch, Dinner).</param>
        /// <param name="dto">The data transfer object containing user ID, timestamp, and food items.</param>
        /// <returns>
        /// Returns <see cref="OkObjectResult"/> with the created meal and its aggregated nutrition data if successful.
        /// Returns <see cref="BadRequestObjectResult"/> if meal creation fails.
        /// </returns>
        /// 
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


        /// <summary>
        /// Updates the details of an existing meal, including its timestamp, type, and food items.
        /// </summary>
        /// <param name="mealId">The unique identifier of the meal to update.</param>
        /// <param name="dto">The updated meal data including timestamp, meal type, and food items.</param>
        /// <returns>
        /// Returns <see cref="OkResult"/> with a success message if update is successful.
        /// Returns <see cref="BadRequestObjectResult"/> if update fails or model state is invalid.
        /// </returns>
        /// 
        [HttpPut("{mealId}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(int mealId, [FromBody] UpdateMealDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool success = await _applicationService.MealService.UpdateAsync(mealId, dto);

            if (!success)
            {
                return BadRequest(new { message = "Failed to Update meal" });
            }

            return Ok(new { message = "meal Updated successfully" });
        }


        /// <summary>
        /// Deletes a specific meal and all its associated food items.
        /// </summary>
        /// <param name="mealId">The unique identifier of the meal to delete.</param>
        /// <returns>
        /// Returns <see cref="OkResult"/> with a success message if deletion is successful.
        /// Returns <see cref="BadRequestObjectResult"/> if deletion fails or model state is invalid.
        /// </returns>
        /// 
        [HttpDelete("{mealId}")]
        [Authorize]
        public async Task<IActionResult> deleteAsync(int mealId)
        {
            bool success = await _applicationService.MealService.DeleteAsync(mealId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!success)
            {
                return BadRequest(new { message = "Failed to delete meal" });
            }

            return Ok(new { message = "meal deleted successfully" });
        }
    }
}