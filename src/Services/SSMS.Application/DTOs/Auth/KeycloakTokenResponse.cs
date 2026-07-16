using System.Text.Json.Serialization;

namespace SSMS.Application.DTOs.Auth
{
    public class KeycloakTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonPropertyName("refresh_expires_in")]
        public int RefreshExpiresIn { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken {  get; set; } = string.Empty;
        [JsonPropertyName("token_type")]
        public string TokenType {  get; set; } = string.Empty;
        //[JsonPropertyName("id_token")]
        //public string IdToken { get; set; } = string.Empty;
        //[JsonPropertyName("scope")]
        //public string Scope { get; set; } = string.Empty;
        [JsonPropertyName("roles")]
        public IEnumerable<string> Roles { get; set; } = [];
        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;
        //[JsonPropertyName("first_name")]
        //public string FirstName { get; set; } = string.Empty;
        //[JsonPropertyName("last_name")]
        //public string LastName { get; set; } = string.Empty;
    }
}
