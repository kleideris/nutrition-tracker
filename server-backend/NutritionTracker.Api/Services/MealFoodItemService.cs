using AutoMapper;
using NutritionTracker.Api.Repositories;
using Serilog;
namespace NutritionTracker.Api.Services
{
    public class MealFoodItemService : IMealFoodItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<MealFoodItemService> _logger;

        public MealFoodItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<MealFoodItemService>();  // creates the Logger for meal food item service using a factory method
        }





    }
}
