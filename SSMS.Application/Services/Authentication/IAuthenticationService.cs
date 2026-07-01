using SSMS.Application.DTOs.Auth;

namespace SSMS.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<KeycloakTokenResponse> ExchangeCodeForTokenAsync(string authorizationCode, string redirectUri, CancellationToken cancellationToken = default);
        Task<KeycloakTokenResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    }
}
