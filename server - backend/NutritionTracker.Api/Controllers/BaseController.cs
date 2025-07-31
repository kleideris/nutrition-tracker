using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Api.Models;
using NutritionTracker.Api.Services;
using System.Security.Claims;

namespace NutritionTracker.Api.Controllers
{
    // [Produces("application/json")] default since it derives from ControllerBase
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IApplicationService _applicationService;

        protected BaseController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        private ApplicationUser? _appUser;

        protected ApplicationUser? AppUser
        {
            get
            {
                if (User != null && User.Claims != null && User.Claims.Any())
                {

                    var claimsTypes = User.Claims.Select(x => x.Type);
                    if (!claimsTypes.Contains(ClaimTypes.NameIdentifier))
                    {
                        return null;
                    }

                    // User is an instance of ClaimsPrincipal
                    var userClaimsId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    _ = int.TryParse(userClaimsId, out int id);

                    _appUser = new ApplicationUser
                    {
                        Id = id
                    };

                    var userClaimsName = User.FindFirst(ClaimTypes.Name)?.Value;

                    _appUser.Username = userClaimsName!;
                    _appUser.Email = User.FindFirst(ClaimTypes.Email)?.Value;
                    return _appUser;
                }
                return null;
            }
        }
    }
}
