using AutoMapper;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Repositories;
using Serilog;

namespace NutritionTracker.Api.Services
{
    public class MealService : IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MealService> _logger;

        public MealService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<MealService>();  // creates the Logger for meal service using a factory method
        }



        //Finished (Adds meal but need to make it so you can only add 1 type of meal per day...)
        public async Task<bool> AddMealAsync(MealType mealType, MealPostDTO dto)
        {
            Meal? meal = _mapper.Map<Meal>(dto);
            meal.MealType = mealType;

            if (meal == null)
            {
                _logger.LogWarning("Meal mapping failed for DTO: {@dto}", dto);
                return false;
            }
            try
            {
                await _unitOfWork.MealRepository.AddMealAsync(meal);
                _logger.LogInformation("Meal: {meal} added successfully", meal);  //TODO: check if this needs ToString to work
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message},{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }


        //Finished with minor bugs
        public async Task<Meal?> GetMealByIdAsync(int mealId) => await _unitOfWork.MealRepository.GetMealByIdAsync(mealId);


        //Finished with minor bugs
        public Task<IEnumerable<Meal>> GetMealsByUserAsync(int userId) => _unitOfWork.MealRepository.GetMealsByUserAsync(userId);
    }
}



