using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace NutritionTracker.Api.DTOs
{
    public class JwtTokenDto
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string? TokenType { get; set; } = "Bearer";
    }
}
