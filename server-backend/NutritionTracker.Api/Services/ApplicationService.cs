using AutoMapper;
using NutritionTracker.Api.Repositories;

namespace NutritionTracker.Api.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserService UserService => new (_unitOfWork, _mapper);
        public MealService MealService => new MealService(_unitOfWork, _mapper);
    }
}
