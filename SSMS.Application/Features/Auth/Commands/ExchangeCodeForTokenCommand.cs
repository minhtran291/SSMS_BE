using MediatR;
using SSMS.Application.DTOs.Auth;
using SSMS.Application.Services.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace SSMS.Application.Features.Auth.Commands
{
    public class ExchangeCodeForTokenCommand : IRequest<KeycloakTokenResponse>
    {
        public required string AuthorizationCode { get; set; }
        public required string RedirectUri { get; set; }
    }

    public class ExchangeCodeForTokenCommandHandler : IRequestHandler<ExchangeCodeForTokenCommand, KeycloakTokenResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public ExchangeCodeForTokenCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<KeycloakTokenResponse> Handle(ExchangeCodeForTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenResponse = await _authenticationService.ExchangeCodeForTokenAsync(request.AuthorizationCode, request.RedirectUri, cancellationToken);

            var claims = DecodeTokenAndGetClaims(tokenResponse.AccessToken);

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

            if(resourceAccessClaim is not null)
            {
                using var resourceAccessJson = JsonDocument.Parse(resourceAccessClaim.Value);

                foreach(var property in resourceAccessJson.RootElement.EnumerateObject())
                {
                    if(property.Value.TryGetProperty("roles", out var clientRolesElement))
                    {
                        foreach(var role in clientRolesElement.EnumerateArray())
                        {
                            allRoles.Add(role.GetString());
                        }
                    }
                }
            }

            return allRoles;
        }
    }
}
