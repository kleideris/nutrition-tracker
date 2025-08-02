using AutoMapper;
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
        public async Task<bool> AddMealAsync(MealPostDTO dto)
        {
            Meal? meal = _mapper.Map<Meal>(dto);

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


        //WIP
        public Task<Meal?> GetMealByIdAsync(int mealId) { throw new NotImplementedException(); }
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("{Message},{Exception}", ex.Message, ex.StackTrace);
        //        throw;
        //    }
        //}


        //WIP
        public Task<IEnumerable<Meal>> GetMealsByUserAsync(int userId) { throw new NotImplementedException(); }
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("{Message},{Exception}", ex.Message, ex.StackTrace);
        //        throw;
        //    }
        //}
    }
}
