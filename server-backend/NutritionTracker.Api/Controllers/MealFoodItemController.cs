using AutoMapper;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    public class MealFoodItemController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public MealFoodItemController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper) : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }



    }
}
