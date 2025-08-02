using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Core.Filters;
using NutritionTracker.Api.Core.Helpers;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Repositories;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace NutritionTracker.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<UserService>();  // creates the Logger for user service using a factory method
        }



        //Finished
        public async Task<List<User>> GetAllUsersFilteredPaginatedAsync(int pageNumber, int pageSize, UserFiltersDTO userFilterDTO)
        {
            try
            {
                Expression<Func<User, bool>> predicates = UserPredicateBuilder.BuildPredicates(userFilterDTO);

                var users = await _unitOfWork.UserRepository
                    .GetAllUsersFilteredPaginatedAsync(pageNumber, pageSize, predicates);
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message},{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }


        //Finished
        public async Task<User?> GetUserByIdAsync(int id) => await _unitOfWork.UserRepository.GetAsync(id);


        //Finished
        public async Task<User?> GetUserByUsernameAsync(string username) => await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);


        //Finished
        public async Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.AuthenticateUserAsync(credentials.UsernameOrEmail!, credentials.Password!);
                _logger.LogInformation("{Message}", $"User: " + user + " found and returned.");  // TODO: needs user ToString for this to work
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
            
        }


        //Finished
        public string CreateUserToken(int userId, string username, string email,
                UserRole userRole, string appSecurityKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsInfo = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, userRole.ToString()!)
            };

            var jwtSecurityToken = new JwtSecurityToken("https://codingfactory.aueb.gr", "https://api.codingfactory.aueb.gr", claimsInfo, DateTime.UtcNow,
                DateTime.UtcNow.AddHours(3), signingCredentials);
            //var jwtSecurityToken = new JwtSecurityToken(null, null, claimsInfo, DateTime.UtcNow,
            //    DateTime.UtcNow.AddHours(3), signingCredentials);

            // Serialize the token
            var userToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return userToken;
        }


        //Finished--
        public async Task<RegisterResult> RegisterUserAsync(UserRegisterDTO dto)
        {
            var existingUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(dto.Username!);
            if (existingUser != null)
            {
                return new RegisterResult { Success = false, ErrorMessage = "Username already in use." };
            }

            var user = _mapper.Map<User>(dto);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return new RegisterResult { Success = true, UserId = user.Id };
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            
            try
            {
                bool deleted = await _unitOfWork.UserRepository.DeleteAsync(id);
                _logger.LogInformation("{Message}", "User with id: " + id + " deleted.");
                await _unitOfWork.SaveAsync();
                return deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}

