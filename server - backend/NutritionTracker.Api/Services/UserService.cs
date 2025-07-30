using AutoMapper;
using NutritionTracker.Api.Core.Filters;
using NutritionTracker.Api.Core.Helpers;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Repositories;
using Serilog;
using System;
using System.Linq.Expressions;

namespace NutritionTracker.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<UserService>();  // create the Logger for user service using a factory method
        }

        public async Task<List<User>> GetAllUsersFilteredPaginatedAsync(int pageNumber, int pageSize, UserFiltersDTO userFilterDTO)
        {
            List<User> users;
            Expression<Func<User, bool>> predicates;

            try
            {
                predicates = UserPredicateBuilder.BuildPredicates(userFilterDTO);

                users = await _unitOfWork.userRepository
                    .GetAllUsersFilteredPaginatedAsync(pageNumber, pageSize, predicates);
            }
            catch (Exception e)
            {
                _logger.LogError("{Message},{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return users;
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials)
        {
            throw new NotImplementedException();
        }                
    }
}

