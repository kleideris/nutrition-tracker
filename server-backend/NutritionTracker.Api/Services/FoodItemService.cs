using AutoMapper;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTOs;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Repositories.Interfaces;
using Serilog;

namespace NutritionTracker.Api.Services
{
    /// <summary>
    /// Service class for managing food items, including creation, deletion, retrieval, and search operations.
    /// </summary>
    /// 
    public class FoodItemService(IUnitOfWork unitOfWork, IMapper mapper) /*: IFoodItemService */
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<FoodItemService> _logger = new LoggerFactory().AddSerilog().CreateLogger<FoodItemService>();


        /// <summary>
        /// Adds a new food item to the database. It also trims the name and makes sure its not whitespace.
        /// </summary>
        /// <param name="dto">The data transfer object containing food item details.</param>
        /// <returns>True if the item was added successfully; otherwise, false.</returns>
        /// <exception cref="EntityAlreadyExistsException">Thrown if a food item with the same name already exists.</exception>
        /// 
        public async Task<bool> AddAsync(FoodItemDto dto)
        {
            dto.Name = dto.Name.Trim();

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new InvalidActionException("Name", "Food name cannot be empty or whitespace.");

            FoodItem foodItem = _mapper.Map<FoodItem>(dto);
            if (foodItem == null)
            {
                _logger.LogWarning("FoodItem mapping failed for DTOs: {@dto}", dto);
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
                _logger.LogInformation("FoodItem: {foodItem} added successfully", foodItem.Name);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }


        /// <summary>
        /// Deletes a food item by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the food item to delete.</param>
        /// <returns>True if the item was deleted successfully; otherwise, false.</returns>
        /// 
        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _unitOfWork.FoodItemRepository.DeleteAsync(id);
            if (deleted)
            {
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("Deleted FoodItem with id: {id}", id);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Searches for food items by name using a partial match.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <returns>A list of food items whose names match the query.</returns>
        /// 
        public async Task<List<FoodItem>> SearchByNameAsync(string query)
        {
            var matches = await _unitOfWork.FoodItemRepository.SearchByNameAsync(query);
            return matches;
        }


        /// <summary>
        /// Retrieves all food items from the database.
        /// </summary>
        /// <returns>A list of all food items.</returns>
        /// 
        public async Task<List<FoodItem>> GetAllAsync() => await _unitOfWork.FoodItemRepository.GetAllAsListAsync();
    }
}
