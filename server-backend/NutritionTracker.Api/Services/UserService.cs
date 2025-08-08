using AutoMapper;
using NutritionTracker.Api.Core.Filters;
using NutritionTracker.Api.Core.Helpers;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Repositories;
using Serilog;
using System.Linq.Expressions;

namespace NutritionTracker.Api.Services
{
    public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<UserService> _logger = new LoggerFactory().AddSerilog().CreateLogger<UserService>();


        public async Task<List<User>> GetFilteredPaginatedAsync(int pageNumber, int pageSize, UserFiltersDTO userFilterDTO)
        {
            Expression<Func<User, bool>> predicates = UserPredicateBuilder.BuildPredicates(userFilterDTO);

            var users = await _unitOfWork.UserRepository
                .GetAllUsersFilteredPaginatedAsync(pageNumber, pageSize, predicates);
            return users;
        }


        public async Task<User?> GetByIdAsync(int id) => await _unitOfWork.UserRepository.GetAsync(id);


        public async Task<User?> GetByUsernameAsync(string username) => await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);


        public async Task<User?> VerifyAndGetAsync(LoginUserDto credentials)
        {
            var user = await _unitOfWork.UserRepository.AuthenticateUserAsync(credentials.UsernameOrEmail!, credentials.Password!) ??
                throw new EntityNotFoundException("User", "User with username: " + credentials.UsernameOrEmail + " could not be found");

            _logger.LogInformation("User Found, Authenticated and returned: {UserId}", user.Id);

            return user;
        }


        //Finished--
        public async Task<RegisterResult> RegisterAsync(RegisterUserDto dto)
        {
            var existingUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(dto.Username!);
            if (existingUser != null)
            {
                return new RegisterResult { Success = false, ErrorMessage = "Username already in use." };
            }

            var user = _mapper.Map<User>(dto);
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User Registered: {UserId}", user.Id);

            return new RegisterResult { Success = true, UserId = user.Id };
        }


        public async Task<User> UpdateAsync(User user, UpdateUserDto dto)
        {
            if (dto.Email == user.Email || dto.Email == null)
            {
                dto.Email = null; // Prevent AutoMapper from overwriting
            }
            else
            {
                var emailExists = await _unitOfWork.UserRepository.EmailExistsAsync(dto.Email);
                if (emailExists)
                    throw new EntityAlreadyExistsException("User", $"Email '{dto.Email}' is already in use.");
            }

            _mapper.Map(dto, user);  // Map all other fields (email will be skipped if null)

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User Updated: {UserId}", user.Id);
            
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            bool deleted = await _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User deleted: {UserId}", id);

            return deleted;
        }
    }
}

