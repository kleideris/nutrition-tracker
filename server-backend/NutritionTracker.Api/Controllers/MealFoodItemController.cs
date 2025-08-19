//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using NutritionTracker.Api.DTOs;
//using NutritionTracker.Api.Services;

//namespace NutritionTracker.Api.Controllers
//{
//    [Route("api/meals/{mealId}/food-items")]
//    [ApiController]
//    public class MealFoodItemController(IApplicationService applicationService, IConfiguration configuration,
//        IMapper mapper) : BaseController(applicationService)
//    {
//        private readonly IConfiguration _configuration = configuration;
//        private readonly IMapper _mapper = mapper;


//        //WIP#
//        [HttpPost("{foodItemId}")]
//        [Authorize]
//        public async Task<IActionResult> AddFoodItemToMeal(int mealId, int foodItemId, [FromBody] AddMealFoodItemDto dto)
//        {
//            var result = await _applicationService.MealFoodItemService.AddFoodItemToMealAsync(mealId, foodItemId, dto.Quantity, dto.Unit);
//            return result ? Ok() : BadRequest();
//        }


//        //WIP#
//        [HttpGet()]
//        [Authorize]
//        public async Task<IActionResult> GetAllMealFoodItems(int mealId)
//        {
//            var items = await _applicationService.MealFoodItemService.GetByMealIdAsync(mealId);
//            return items != null ? Ok(items) : NotFound();
//        }
            


//        //WIP#
//        [HttpDelete("{foodItemId}")]
//        [Authorize]
//        public async Task<IActionResult> DeleteFoodItemFromMeal(int mealId, int foodItemId)
//        {
//            var result = await _applicationService.MealFoodItemService.DeleteFoodItemOfMealAsync(mealId, foodItemId, 0, "");
//            return result ? Ok() : NotFound();
//        }


//        //WIP#
//        [HttpGet("{foodItemId}")]
//        [Authorize]
//        public async Task<IActionResult> GetMealFoodItem(int mealId, int foodItemId)
//        {
//            var item = await _applicationService.MealFoodItemService.GetByJoinedIdsAsync(mealId, foodItemId);
//            return item != null ? Ok(item) : NotFound();
//        }


//        //WIP#
//        [HttpPut("{foodItemId}")]
//        [Authorize]
//        public async Task<IActionResult> UpdateQuantity(int mealId, int foodItemId, [FromBody] UpdateMealFoodItemDto dto)
//        {
//            var result = await _applicationService.MealFoodItemService.UpdateQuantityOfFoodItemAsync(mealId, foodItemId, dto.Quantity, dto.Unit);
//            return result ? Ok() : NotFound();
//        }

//    }
//}
