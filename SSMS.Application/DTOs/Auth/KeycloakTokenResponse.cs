using System.Text.Json.Serialization;

namespace SSMS.Application.DTOs.Auth
{
    public class KeycloakTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonPropertyName("refresh_expires_in")]
        public int RefreshExpiresIn { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken {  get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType {  get; set; }
        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
        [JsonPropertyName("roles")]
        public IEnumerable<string> Roles { get; set; } = [];
    }
}
