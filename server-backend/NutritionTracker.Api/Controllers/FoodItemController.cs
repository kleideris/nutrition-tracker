using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    public class FoodItemController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public FoodItemController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper) : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }



        //Finished
        [HttpPost]
        public async Task<IActionResult> AddFoodItem(FoodItemDTO dto)
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


        //Finished
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodItemById(int id)
        {
            FoodItem foodItem = await _applicationService.FoodItemService.GetFoodItemByIdAsync(id) ?? throw new EntityNotFoundException("FoodItem", "FoodItem: " + id + " NotFound");
            var returnedDto = _mapper.Map<FoodItemDTO>(foodItem);
            return Ok(returnedDto);
        }


        //Finished
        [HttpGet("{name}")]
        public async Task<IActionResult> GetFoodItemByName(string name)
        {
            FoodItem foodItem = await _applicationService.FoodItemService.GetFoodItemByNameAsync(name) ?? throw new EntityNotFoundException("FoodItem", "FoodItem: " + name + " NotFound");
            var returnedDto = _mapper.Map<FoodItemDTO>(foodItem);
            return Ok(returnedDto);
        }


        //Finished
        [HttpGet]
        public async Task<IActionResult> GetAllFoodItems()
        {
            List<FoodItem?> foodItems = await _applicationService.FoodItemService.GetAllFoodItemsAsync();
            var returnedDtos = foodItems.Select(f => _mapper.Map<FoodItemDTO>(f)).ToList();
            return Ok(returnedDtos);

        }
    }
}
