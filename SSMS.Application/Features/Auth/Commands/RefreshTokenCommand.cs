using MediatR;
using SSMS.Application.DTOs.Auth;
using SSMS.Application.Exceptions;
using SSMS.Application.Services.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace SSMS.Application.Features.Auth.Commands
{
    public record RefreshTokenCommand : IRequest<KeycloakTokenResponse>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, KeycloakTokenResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        public RefreshTokenCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<KeycloakTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                throw new UnauthorizedException("Không tìm thấy refresh token.");

            var tokenResponse = await _authenticationService.RefreshTokenAsync(request.RefreshToken, cancellationToken);

            var claims = DecodeTokenAndGetClaims(tokenResponse.AccessToken);

            var firstName = claims.FirstOrDefault(c => c.Type == "given_name")?.Value ?? "";

            var lastName = claims.FirstOrDefault(c => c.Type == "family_name")?.Value ?? "";

            tokenResponse.FullName = $"{lastName} {firstName}";

            tokenResponse.Roles = ExtractRolesFromClaims(claims);

            return tokenResponse;
        }

        private List<Claim> DecodeTokenAndGetClaims(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                return [];

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);
            return [.. jwtToken.Claims];
        }

        private IEnumerable<string> ExtractRolesFromClaims(List<Claim> claims)
        {
            var allRoles = new HashSet<string>();

            var resourceAccessClaim = claims.FirstOrDefault(c => c.Type == "resource_access");

            if (resourceAccessClaim is not null)
            {
                using var resourceAccessJson = JsonDocument.Parse(resourceAccessClaim.Value);

                foreach (var property in resourceAccessJson.RootElement.EnumerateObject())
                {
                    if (property.Value.TryGetProperty("roles", out var clientRolesElement))
                    {
                        foreach (var role in clientRolesElement.EnumerateArray())
                        {
                            if (role.GetString() is string roleName)
                                allRoles.Add(roleName);
                        }
                    }
                }
            }

            return allRoles;
        }
    }
}
