using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    /// <summary>
    /// Handles login requests and issues JWT tokens for authenticated users.
    /// </summary>
    /// 
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IApplicationService applicationService, IConfiguration configuration,
        IMapper mapper) : BaseController(applicationService)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;


        /// <summary>
        /// Authenticates user credentials and returns a JWT token for authorized access.
        /// </summary>
        /// <param name="credentials">User credentials including username and password.</param>
        /// <returns>A JWT token if authentication is successful.</returns>
        /// <response code="200">Returns the JWT token.</response>
        /// <response code="401">If the credentials are invalid.</response>
        /// <exception cref="EntityNotAuthorizedException">
        /// Thrown when username or password is invalid.
        /// </exception>
        /// 
        [HttpPost("login/access-token")]
        public async Task<ActionResult<JwtTokenDto>> Login([FromBody] LoginUserDto credentials)
        {
            var user = await _applicationService.UserService.VerifyAndGetAsync(credentials) ??
                throw new EntityNotAuthorizedException("User", "Bad credentials. Username or password did not match");

            var userToken = _applicationService.AuthService
                .CreateUserToken(user.Id, user.Username!, user.Email!, user.UserRole, _configuration["Authentication:SecretKey"]!);

            JwtTokenDto accessToken = new() 
            { 
                AccessToken = userToken,
                TokenType = "Bearer"
            };

            return Ok(accessToken);
        }
    }
}
