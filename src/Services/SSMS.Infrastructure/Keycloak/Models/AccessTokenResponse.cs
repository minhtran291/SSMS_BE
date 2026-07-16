using System.Text.Json.Serialization;

namespace SSMS.Infrastructure.Keycloak.Models
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;
    }
}
