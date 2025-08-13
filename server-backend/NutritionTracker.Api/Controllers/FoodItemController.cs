using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    /// <summary>
    /// Controller for managing food items in the nutrition tracker.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="FoodItemController"/>.
    /// </remarks>
    [Route("api/food-items/")]
    [ApiController]
    public class FoodItemController(IApplicationService applicationService, IConfiguration configuration,
        IMapper mapper) : BaseController(applicationService)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;


        /// <summary>
        /// Adds a new food item to the tracker.
        /// </summary>
        /// <param name="dto">The food item data transfer object.</param>
        /// <returns>Returns a success or failure message.</returns>
        [HttpPost]
        public async Task<IActionResult> AddFoodItem(FoodItemDto dto)
        {
            bool success = await _applicationService.FoodItemService.AddFoodItemAsync(dto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!success)
            {
                return BadRequest(new { message = "Failed to log food item" });
            }

            return Ok(new { message = "food item logged successfully" });
        }

        /// <summary>
        /// Retrieves a food item by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the food item.</param>
        /// <returns>The food item DTO if found.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFoodItemById(int id)
        {
            FoodItem foodItem = await _applicationService.FoodItemService.GetFoodItemByIdAsync(id) ?? throw new EntityNotFoundException("FoodItem", "FoodItem: " + id + " NotFound");
            var returnedDto = _mapper.Map<FoodItemDto>(foodItem);
            return Ok(returnedDto);
        }

        /// <summary>
        /// Retrieves a food item by its name.
        /// </summary>
        /// <param name="name">The name of the food item.</param>
        /// <returns>The food item DTO if found.</returns>
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            FoodItem? foodItem = await _applicationService.FoodItemService.GetByNameAsync(name);

            var returnedDto = _mapper.Map<FoodItemDto>(foodItem);
            return Ok(returnedDto);
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchFoodItems([FromQuery] string query)
        {
            var matches = await _applicationService.FoodItemService.SearchByNameAsync(query);
            var returnedDtos = matches.Select(f => _mapper.Map<FoodItemDto>(f)).ToList();
            return Ok(returnedDtos);
        }

        /// <summary>
        /// Retrieves all food items in the tracker.
        /// </summary>
        /// <returns>A list of food item DTOs.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllFoodItems()
        {
            List<FoodItem?> foodItems = await _applicationService.FoodItemService.GetAllFoodItemsAsync();
            var returnedDtos = foodItems.Select(f => _mapper.Map<FoodItemDto>(f)).ToList();
            return Ok(returnedDtos);
        }
    }
}
