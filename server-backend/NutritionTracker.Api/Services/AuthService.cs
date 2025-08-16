using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using NutritionTracker.Api.Core.Enums;
using NutritionTracker.Api.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NutritionTracker.Api.Services
{
    /// <summary>
    /// Provides authentication-related services such as JWT token generation.
    /// </summary>
    /// 
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="email">The email address of the user.</param>
        /// <param name="userRole">The role assigned to the user.</param>
        /// <param name="appSecurityKey">The secret key used to sign the token.</param>
        /// <returns>A signed JWT token string.</returns>
        /// <remarks>
        /// The token includes claims for username, user ID, email, and role.
        /// The issuer and audience are set to example URLs; use <c>null</c> if not needed.
        /// </remarks>
        /// 
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

            // You can replace the issuer and audience with null if you're not validating them
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "https://codingfactory.aueb.gr",
                audience: "https://api.codingfactory.aueb.gr",
                claims: claimsInfo,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: signingCredentials);

            var userToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return userToken;
        }
    }
}
