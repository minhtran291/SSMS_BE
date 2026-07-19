using MediatR;
using SSMS.Application.DTOs.Auth;
using SSMS.Application.Exceptions;
using SSMS.Application.Services.Authentication;
using SSMS.Domain;
using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace SSMS.Application.Features.Auth.Commands
{
    public class ExchangeCodeForTokenCommand : IRequest<KeycloakTokenResponse>
    {
        public string AuthorizationCode { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
    }

    public class ExchangeCodeForTokenCommandHandler : IRequestHandler<ExchangeCodeForTokenCommand, KeycloakTokenResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExchangeCodeForTokenCommandHandler(
            IAuthenticationService authenticationService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<KeycloakTokenResponse> Handle(ExchangeCodeForTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.AuthorizationCode) || string.IsNullOrEmpty(request.RedirectUri))
                throw new BadRequestException("Không tìm thấy authorization code hoặc redirect uri.");

            var tokenResponse = await _authenticationService.ExchangeCodeForTokenAsync(request.AuthorizationCode, request.RedirectUri, cancellationToken);

            var claims = DecodeTokenAndGetClaims(tokenResponse.AccessToken);

            var firstName = claims.FirstOrDefault(c => c.Type == "given_name")?.Value ?? "";

            var lastName = claims.FirstOrDefault(c => c.Type == "family_name")?.Value ?? "";

            tokenResponse.FullName = $"{lastName} {firstName}";

            tokenResponse.Roles = ExtractRolesFromClaims(claims);

            await ProvisionUserAsync(claims, cancellationToken);

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

        private async Task ProvisionUserAsync(List<Claim> claims, CancellationToken cancellationToken)
        {
            var keycloakId = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(keycloakId))
                throw new InvalidOperationException("Token không chứa 'sub' claim.");

            var isExist = await _userRepository.AnyAsync(u => u.KeycloakId == keycloakId, cancellationToken: cancellationToken);

            if (!isExist)
            {
                var user = new User
                {
                    KeycloakId = keycloakId,
                    Username = claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value ?? "",
                    Email = claims.FirstOrDefault(c => c.Type == "email")?.Value ?? "",
                    FirstName = claims.FirstOrDefault(c => c.Type == "given_name")?.Value ?? "",
                    LastName = claims.FirstOrDefault(c => c.Type == "family_name")?.Value ?? "",
                    AvatarUrl = null,
                    AvatarPublicId = null,
                };

                await _userRepository.InsertAsync(user, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
