using Microsoft.Extensions.Options;
using SSMS.Application.DTOs.Auth;
using SSMS.Application.Exceptions;
using SSMS.Application.Services.Authentication;
using SSMS.Domain.ConfigOptions;
using System.Net.Http.Json;
using System.Text.Json;

namespace SSMS.Infrustructure.Services.Authentication
{
    public class KeycloakAuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly KeycloakSettings _keycloakSettings;

        public KeycloakAuthenticationService(HttpClient httpClient, IOptions<KeycloakSettings> keycloakOptions)
        {
            _httpClient = httpClient;
            _keycloakSettings = keycloakOptions.Value;
        }
        public async Task<KeycloakTokenResponse> ExchangeCodeForTokenAsync(string authorizationCode, string redirectUri, CancellationToken cancellationToken)
        {
            var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("client_id", _keycloakSettings.ClientId),
                new KeyValuePair<string, string>("client_secret", _keycloakSettings.ClientSecret)
            ]);

            var response = await _httpClient.PostAsync(
                $"{_keycloakSettings.AuthServerUrl}/realms/{_keycloakSettings.Realm}/protocol/openid-connect/token",
                content,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new BadRequestException($"Lỗi khi trao đổi token: {errorContent}");
            }

            //var json = await response.Content.ReadAsStringAsync(cancellationToken);

            //JsonDocument document = JsonDocument.Parse(json);

            //foreach(var property in document.RootElement.EnumerateObject())
            //{
            //    Console.WriteLine($"{property.Name} = {property.Value}");
            //}

            //var tokenResponse = JsonSerializer.Deserialize<KeycloakTokenResponse>(json);

            var tokenResponse = await response.Content.ReadFromJsonAsync<KeycloakTokenResponse>(cancellationToken);
            return tokenResponse ?? throw new BadRequestException("Không nhập được phản hồi token hợp lệ.");
        }

        public async Task<KeycloakTokenResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("client_id", _keycloakSettings.ClientId),
                new KeyValuePair<string, string>("client_secret", _keycloakSettings.ClientSecret),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            ]);

            var response = await _httpClient.PostAsync(
                $"{_keycloakSettings.AuthServerUrl}/realms/{_keycloakSettings.Realm}/protocol/openid-connect/token",
                content,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new BadRequestException($"Lỗi khi trao đổi token: {errorContent}");
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<KeycloakTokenResponse>(cancellationToken);
            return tokenResponse ?? throw new BadRequestException("Lôi khi làm mới token.");
        }
    }
}
