using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.Core.Filters;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.DTO;
using NutritionTracker.Api.Exceptions;
using NutritionTracker.Api.Services;

namespace NutritionTracker.Api.Controllers
{
    /// <summary>
    /// Controller for managing users in the nutrition tracker
    /// </summary>
    /// 
    [Route("users/")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="applicationService">The application service used to provide core application functionality.</param>
        /// <param name="configuration">The configuration instance used to access application settings.</param>
        /// <param name="mapper">The mapper instance used for object-to-object mapping.</param>
        /// 
        public UserController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper) : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }


        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve. Must be a positive integer.</param>
        /// <returns>An <see cref="ActionResult{T}"/> containing a <see cref="UserReadOnlyDto"/> representing the user if found.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if no user with the specified <paramref name="id"/> exists.</exception>
        /// 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserReadOnlyDto>> GetById(int id)
        {
            var user = await _applicationService.UserService.GetByIdAsync(id) ?? throw new EntityNotFoundException("User", "User: " + id + " NotFound");
            var returnedDto = _mapper.Map<UserReadOnlyDto>(user);
            return Ok(returnedDto);
        }


        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve. This value cannot be null or empty.</param>
        /// <returns>An <see cref="ActionResult{T}"/> containing a <see cref="UserReadOnlyDto"/> representing the user with the
        /// specified username. If the user is not found, an exception is thrown.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if no user with the specified <paramref name="username"/> exists.</exception>
        /// 
        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserReadOnlyDto>> GetByUsername(string username)
        {
            var user = await _applicationService.UserService.GetByUsernameAsync(username) ?? throw new EntityNotFoundException("User", "User: " + username + " NotFound");
            var returnedDto = _mapper.Map<UserReadOnlyDto>(user);
            return Ok(returnedDto);
        }


        /// <summary>
        /// Retrieves a paginated list of users that match the specified filter criteria.
        /// </summary>
        /// <remarks>This method supports filtering and pagination to efficiently retrieve subsets of user
        /// data. Ensure that <paramref name="pageNumber"/> and <paramref name="pageSize"/> are positive integers to
        /// avoid invalid requests.</remarks>
        /// <param name="userFilterDTO">An object containing the filter criteria to apply when retrieving users.  This may include properties such
        /// as name, role, or other user attributes.</param>
        /// <param name="pageNumber">The page number to retrieve. Must be a positive integer. Defaults to 1.</param>
        /// <param name="pageSize">The number of users to include in each page. Must be a positive integer. Defaults to 10.</param>
        /// <returns>An <see cref="IActionResult"/> containing a paginated list of users that match the filter criteria. The
        /// response is returned as an HTTP 200 OK result with the list of users in the response body.</returns>
        /// 
        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredPaginated([FromQuery] UserFiltersDTO userFilterDTO, [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10)
        {
            var users = await _applicationService.UserService
                .GetFilteredPaginatedAsync(pageNumber, pageSize, userFilterDTO);

            return Ok(users);
        }


        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <remarks>This method validates the provided registration details and attempts to register a
        /// new user.  If the registration is successful, it returns a 201 Created response with the user ID and a
        /// success message.  If the registration fails, it returns a 400 Bad Request response with an error
        /// message.</remarks>
        /// <param name="registerDTO">The user registration details provided in the request body.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see
        /// cref="BadRequestObjectResult"/> if the model state is invalid or the registration fails.  Returns <see
        /// cref="CreatedAtActionResult"/> if the registration is successful, including the user ID and a success
        /// message.</returns>
        /// 
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _applicationService.UserService.RegisterAsync(registerDTO);
            if (!result.Success)  return BadRequest(new { message = result.ErrorMessage });

            return CreatedAtAction(nameof(Register), new { id = result.UserId }, new { message = "User registered successfully" });
        }


        /// <summary>
        /// Deletes a user by their unique identifier.
        /// Only accessible to users with the "Admin" role.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>
        /// Returns 200 OK if the user is deleted successfully; 
        /// otherwise, returns 404 Not Found if the user does not exist.
        /// </returns>
        ///
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _applicationService.UserService.DeleteUserAsync(id);
            if (!result) return NotFound();

            return Ok(new { message = $"User with ID {id} was successfully deleted." });
        }


        /// <summary>
        /// Updates the details of an existing user identified by their username.
        /// Accessible to users with the "Admin" role or the user themselves.
        /// </summary>
        /// <param name="username">The username of the user to update.</param>
        /// <param name="dto">The updated user information.</param>
        /// <returns>Returns the updated user data.</returns>
        /// <response code="200">Returns the updated user data if successful.</response>
        /// <response code="403">Returned if the user is not authorized to update the specified profile.</response>
        /// <response code="401">Returned if the user is not authenticated.</response>
        /// <exception cref="EntityNotFoundException">
        /// Thrown when the specified user cannot be found.
        /// </exception>
        /// 
        [HttpPatch("update/{username}")]
        [Authorize]
        public async Task<IActionResult> Update(string username, [FromBody] UpdateUserDto dto)
        {
            if (AppUser == null) 
                return Unauthorized();

            if (AppUser?.Username != username && !User.IsInRole("Admin"))
                return Forbid("You can only update your own profile.");

            var user = await _applicationService.UserService.GetByUsernameAsync(username);
            if (user == null) 
                throw new EntityNotFoundException("User", "User with username " + username + " could not be found.");
            
            user = await _applicationService.UserService.UpdateAsync(user,  dto);
            var returnedDto = _mapper.Map<UpdateUserDto>(user);

            return Ok(new { status = true, data = returnedDto });
        }
    }
}

