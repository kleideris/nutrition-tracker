using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    [Route("/api/meals")]
    [ApiController]
    public class MealController(IApplicationService applicationService, IConfiguration configuration,
        IMapper mapper) : BaseController(applicationService)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;


        [HttpPost("mealtype/{mealType}")]
        public async Task<IActionResult> AddMeal(MealType mealType, [FromBody] MealPostDto dto)
        {
            var existingMeals = await _applicationService.MealService.GetMealsByUserAsync(dto.UserId);
            bool alreadyLogged = existingMeals.Any(m =>
                m.MealType == mealType &&
                m.Timestamp.Date == dto.Timestamp.Date);
            if (alreadyLogged)
                return BadRequest(new { message = $"Meal of type {mealType} already logged for this day." });


            var createdMeal = await _applicationService.MealService.AddMealAsync(mealType, dto);
            if (createdMeal == null)
                return BadRequest(new { message = "Failed to log meal." });


            // Step 3: Retrieve the newly created meal (assuming latest meal for user is the one just added)
            var meals = await _applicationService.MealService.GetMealsByUserAsync(dto.UserId);
            var meal = meals
                .Where(m => m.MealType == mealType && m.Timestamp.Date == dto.Timestamp.Date)
                .OrderByDescending(m => m.Timestamp)
                .FirstOrDefault();

            if (meal == null)
                return StatusCode(500, new { message = "Meal was created but could not be retrieved." });

            // Step 4: Create MealFoodItems and associate them with the meal
            var mealFoodItems = await _applicationService.MealFoodItemService.CreateMealFoodItemsAsync(dto.MealFoodItems);
            foreach (var item in mealFoodItems)
            {
                item.MealId = meal.Id;
                await _applicationService.MealFoodItemService.AddFoodItemToMealAsync(item.MealId, item.FoodItemId, item.Quantity, item.UnitOfMeasurement);
            }

            return Ok(new
            {
                message = "Meal logged successfully",
                meal,
                foodItems = mealFoodItems
            });

        }


        //Finished  (Adds meal but need to make it so you can only add 1 type of meal per day...)
        //[HttpPost("mealtype/{mealType}")]
        //public async Task<IActionResult> AddMeal(MealType mealType, [FromBody] MealPostDto dto)
        //{
        //    bool success = await _applicationService.MealService.AddMealAsync(mealType, dto);

        //    if (!success) return BadRequest(new { message = "Failed to log meal" });

        //    return Ok(new { message = "Meal logged successfully" });
        //}


        //Finished with minor bugs
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMealsById(int id)
        {
            var meal = await _applicationService.MealService.GetMealByIdAsync(id) ?? throw new EntityNotFoundException("Meal", "Meal: " + id + " NotFound");
            var returnedDto = _mapper.Map<MealReadOnlyDto>(meal);
            return Ok(returnedDto);
        }


        //Finished with minor bugs
        [HttpGet("userid/{userId}")]
        public async Task<IActionResult> GetMealsByUser(int userId)
        {
            var meals = await _applicationService.MealService.GetMealsByUserAsync(userId) ?? throw new EntityNotFoundException("Meal", "Meal: " + userId + " NotFound");
            return Ok(meals);
        }
    }
}