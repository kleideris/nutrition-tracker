using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.Controllers;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    public class MealController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public MealController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper) : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }



        //Finished with minor bugs
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMealsById(int id)
        {
            var meal = await _applicationService.MealService.GetMealByIdAsync(id) ?? throw new EntityNotFoundException("Meal", "Meal: " + id + " NotFound");
            var returnedDto = _mapper.Map<MealReadOnlyDTO>(meal);
            return Ok(returnedDto);
        }


        //Finished with minor bugs
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMealsByUser(int userId)
        {
            var meals = await _applicationService.MealService.GetMealsByUserAsync(userId) ?? throw new EntityNotFoundException("Meal", "Meal: " + userId + " NotFound");
            return Ok(meals);
        }


        //Finished  (Adds meal but need to make it so you can only add 1 type of meal per day...)
        [HttpPost("{mealType}")]
        public async Task<IActionResult> AddMeal(MealType mealType, [FromBody] MealPostDTO dto)
        {
            bool success = await _applicationService.MealService.AddMealAsync(mealType, dto);

            if (!success)
            {
                return BadRequest(new { message = "Failed to log meal" });
            }

            return Ok(new { message = "Meal logged successfully" });
        }
    }
}



//[Route("api/[controller]/[action]")]
//[ApiController]
//public class MealController : BaseController
//{
//    private readonly IMapper _mapper;

//    public MealController(IApplicationService applicationService, IMapper mapper)
//        : base(applicationService)
//    {
//        _mapper = mapper;
//    }

//    [HttpPost]
//    public async Task<IActionResult> Create([FromBody] MealPostDTO dto)
//    {
//        var meal = new Meal
//        {
//            UserId = dto.UserId,
//            MealType = dto.MealType,
//            Timestamp = dto.Timestamp,
//            MealFoodItems = dto.FoodItems.Select(fi => new MealFoodItem
//            {
//                FoodItemId = fi.FoodItemId,
//                Quantity = fi.Quantity,
//                UnitOfMeasurement = fi.UnitOfMeasurement
//            }).ToList()
//        };

//        await _applicationService.MealService.AddMealAsync(meal);
//        return Ok(new { message = "Meal logged successfully" });
//    }

//    [HttpGet("{userId}")]
//    public async Task<IActionResult> User(int userId)
//    {
//        var meals = await _applicationService.MealService.GetMealsByUserAsync(userId);
//        return Ok(meals);
//    }
//}