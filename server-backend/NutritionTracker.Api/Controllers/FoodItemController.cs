using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTOs;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    /// <summary>
    /// Controller for managing food items in the nutrition tracker.
    /// </summary>
    /// 
    [Route("api/food-items")]
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
        /// <returns>
        /// Returns <see cref="OkResult"/> if successful, or <see cref="BadRequestResult"/> with error details.
        /// </returns>
        /// 
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFoodItem(FoodItemDto dto)
        {
            bool success = await _applicationService.FoodItemService.AddAsync(dto);

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
        /// Deletes a food item by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the food item to delete.</param>
        /// <returns>
        /// Returns <see cref="OkResult"/> if deletion is successful, or <see cref="BadRequestResult"/> if it fails.
        /// </returns>
        /// 
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteById(int id)
        {
            bool success = await _applicationService.FoodItemService.DeleteAsync(id);

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
        /// Searches for food items by name.
        /// </summary>
        /// <param name="query">The search query string. If null or empty, all items are returned.</param>
        /// <returns>
        /// A list of matching food items as <see cref="FoodItemDto"/> objects.
        /// </returns>
        /// 
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchFoodItems([FromQuery] string? query)
        {
            List<FoodItem> matches;

            if (string.IsNullOrWhiteSpace(query))
            {
                matches = await _applicationService.FoodItemService.GetAllAsync();
            }
            else
            {
                matches = await _applicationService.FoodItemService.SearchByNameAsync(query);
            }

            var returnedDtos = matches.Select(f => _mapper.Map<FoodItemDto>(f)).ToList();
            return Ok(returnedDtos);
        }
    }
}
