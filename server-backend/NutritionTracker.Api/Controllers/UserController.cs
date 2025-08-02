using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NutritionTracker.Api.Core.Filters;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper) : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }



        //Finished
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUserById(int id)
        {
            var user = await _applicationService.UserService.GetUserByIdAsync(id) ?? throw new EntityNotFoundException("User", "User: " + id + " NotFound");
            var returnedDto = _mapper.Map<UserReadOnlyDTO>(user);
            return Ok(returnedDto);
        }


        //Finished
        [HttpGet("{username}")]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUserByUsername(string username)
        {
            var user = await _applicationService.UserService.GetUserByUsernameAsync(username) ?? throw new EntityNotFoundException("User", "User: " + username + " NotFound");
            var returnedDto = _mapper.Map<UserReadOnlyDTO>(user);
            return Ok(returnedDto);
        }


        //Finished--
        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredUsersPaginated([FromQuery] UserFiltersDTO userFilterDTO, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var users = await _applicationService.UserService
                    .GetAllUsersFilteredPaginatedAsync(pageNumber, pageSize, userFilterDTO);

                return Ok(users);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error retrieving filtered users: {Message}", ex.Message);
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }


        //Finished--
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _applicationService.UserService.RegisterUserAsync(registerDTO);

                if (!result.Success)
                {
                    return BadRequest(new { message = result.ErrorMessage });
                }

                //_logger.LogInformation("User registered with email {Email}", registerDTO.Email);
                return CreatedAtAction(nameof(RegisterUser), new { id = result.UserId }, new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error registering user with email {Email}", registerDTO.Email);
                return StatusCode(500, "An error occurred while registering the user.");
            }

        }


        //Finished
        [HttpPost]
        public async Task<ActionResult<JwtTokenDTO>> LoginUser(UserLoginDTO credentials)
        {
            var user = await _applicationService.UserService.VerifyAndGetUserAsync(credentials);

            if (user == null)
            {
                throw new EntityNotAuthorizedException("User", "Bad credentials. Username or password did not match");
            }

            var userToken = _applicationService.UserService.CreateUserToken(user.Id, user.Username!, user.Email!,
                user.UserRole, _configuration["Authentication:SecretKey"]!);

            JwtTokenDTO token = new ()
            {
                Token = userToken
            };

            return Ok(token);
        }


        //Finished--
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _applicationService.UserService.DeleteUserAsync(id);
                if (!result)
                    return NotFound();

                return Ok(new { message = $"User with ID {id} was successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while Deleting the user.");
            }
            
        }              
    }
}
