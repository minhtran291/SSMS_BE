using System.Text.Json.Serialization;

namespace SSMS.Infrustructure.Keycloak.Models
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;
    }
}
