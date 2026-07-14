using Shared.Contracts.Authentication;
using SSMS.Application.Services.User;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SSMS.Infrustructure.Services.User
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Lazy<CurrentUser?> _currentUser;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _currentUser = new Lazy<CurrentUser?>(() =>
            {
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext?.User.Identity?.IsAuthenticated != true)
                    return null;

                var userPrincipal = httpContext.User;

                //Console.WriteLine($"IsAuthenticated: {userPrincipal.Identity?.IsAuthenticated}");

                //foreach(var claim in userPrincipal.Claims)
                //{
                //    Console.WriteLine($"{claim.Type} = {claim.Value}");
                //}

                var keycloakId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

                //Console.WriteLine($"sub = {keycloakId}");

                if (string.IsNullOrEmpty(keycloakId))
                    return null;

                var username = userPrincipal.FindFirstValue("preferred_username") ?? string.Empty;

                var givenName = userPrincipal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty;

                var familyName = userPrincipal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty;

                return new CurrentUser
                {
                    KeycloakUserId = keycloakId,
                    UserName = username,
                    FullName = $"{familyName} {givenName}".Trim(),
                };
            });
        }
        public CurrentUser? User => _currentUser.Value;
    }
}
