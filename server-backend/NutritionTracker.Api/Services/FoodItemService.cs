using AutoMapper;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Repositories;
using Serilog;

namespace NutritionTracker.Api.Services
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FoodItemService> _logger;

        public FoodItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<FoodItemService>();  // creates the Logger for food item service using a factory method
        }


        //Finished
        public async Task<bool> AddFoodItemAsync(FoodItemDto dto)
        {
            FoodItem foodItem = _mapper.Map<FoodItem>(dto);
            if (foodItem == null)
            {
                _logger.LogWarning("FoodItem mapping failed for DTO: {@dto}", dto);
                return false;
            }

            var existing = await _unitOfWork.FoodItemRepository.GetByNameAsync(dto.Name);
            if (existing != null)
            {
                throw new EntityAlreadyExistsException("FoodItem", "FoodItem: " + foodItem.Name + " already exists");
            }

            try
            {
                await _unitOfWork.FoodItemRepository.AddAsync(foodItem);
                _logger.LogInformation("FoodItem: {foodItem} added successfully", foodItem);  //TODO: check if this needs ToString to work
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }


        //Finished
        public async Task<List<FoodItem?>> GetAllFoodItemsAsync() => await _unitOfWork.FoodItemRepository.GetAllAsListAsync();


        //Finished
        public async Task<FoodItem?> GetFoodItemByIdAsync(int id)
        {
            try
            {
                FoodItem? foodItem = await _unitOfWork.FoodItemRepository.GetAsync(id);
                if (foodItem == null)
                {
                    throw new EntityNotFoundException("FoodItem", "Food item with id " + id + " was not found");
                }
                return foodItem;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }


        public async Task<FoodItem?> GetByNameAsync(string name)
        {
                FoodItem? foodItem = await _unitOfWork.FoodItemRepository.GetByNameAsync(name);
                if (foodItem == null)
                {
                    throw new EntityNotFoundException("FoodItem", "Food item with name " + name + " was not found");
                }
                return foodItem;
        }
    }
}
