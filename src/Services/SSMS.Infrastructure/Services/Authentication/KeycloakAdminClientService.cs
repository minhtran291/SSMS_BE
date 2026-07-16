using Microsoft.Extensions.Options;
using SSMS.Application.Services.Authentication;
using SSMS.Domain.ConfigOptions;
using SSMS.Infrastructure.Keycloak.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using SSMS.Application.Exceptions;

namespace SSMS.Infrastructure.Services.Authentication
{
    public class KeycloakAdminClientService : IKeycloakAdminClientService
    {
        private readonly HttpClient _httpClient;
        private readonly KeycloakSettings _keycloakSettings;

        public KeycloakAdminClientService(HttpClient httpClient, IOptions<KeycloakSettings> keycloakSettings)
        {
            _httpClient = httpClient;
            _keycloakSettings = keycloakSettings.Value;
        }

        private async Task<string> GetAdminAccessTokenAsync(CancellationToken cancellationToken)
        {
            // tao body
            var content = new FormUrlEncodedContent(
                [
                    new KeyValuePair<string, string>("client_id", _keycloakSettings.AdminClientId),
                    new KeyValuePair<string, string>("client_secret", _keycloakSettings.AdminClientSecret),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                ]);

            // gui request
            var response = await _httpClient.PostAsync(
                $"{_keycloakSettings.AuthServerUrl}/realms/{_keycloakSettings.Realm}/protocol/openid-connect/token",
                content,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<AccessTokenResponse>(cancellationToken);

            if (tokenResponse is null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                throw new InvalidOperationException("Failed to retrieve admin access token from Keycloak");

            return tokenResponse.AccessToken;
        }

        public async Task LogoutUserAsync(string keycloakUserId, CancellationToken cancellationToken)
        {
            // lay access token
            var accessToken = await GetAdminAccessTokenAsync(cancellationToken);

            // tao url
            string url = $"{_keycloakSettings.AuthServerUrl}/admin/realms/{_keycloakSettings.Realm}/users/{keycloakUserId}/logout";

            // tao request
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            // gan access token
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // goi api gui request
            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
                return;

            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new NotFoundException("Người dùng không tồn tại trên hệ thống.");

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new ForbiddenException("Admin client không có quyền thực hiện thao tác này.");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedException("Không thể xác thực với Keycloak.");

            throw new InvalidOperationException($"Failed to logout user in Keycloak. Status: {response.StatusCode}, Response: {errorContent}");
        }
    }
}
