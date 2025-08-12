using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace NutritionTracker.Api.DTO
{
    public class JwtTokenDto
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string? TokenType { get; set; } = "Bearer";

        //[JsonPropertyName("firstname")]
        //public string? Firstname { get; set; }

        //[JsonPropertyName("lastname")]
        //public string? Lastname { get; set; }
    }
}
