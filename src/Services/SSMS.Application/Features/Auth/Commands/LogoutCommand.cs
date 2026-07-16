using MediatR;
using SSMS.Application.Services.Authentication;
using SSMS.Application.Services.User;
using SSMS.Application.Exceptions;

namespace SSMS.Application.Features.Auth.Commands
{
    public class LogoutCommand : IRequest;

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IKeycloakAdminClientService _keycloakAdminClientService;
        private readonly ICurrentUserService _currentUserService;

        public LogoutCommandHandler(IKeycloakAdminClientService keycloakAdminClientService, ICurrentUserService currentUserService)
        {
            _keycloakAdminClientService = keycloakAdminClientService;
            _currentUserService = currentUserService;
        }

        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.User
                ?? throw new UnauthorizedException("Người dùng chưa đăng nhập");

            var keycloakId = currentUser.KeycloakUserId;

            await _keycloakAdminClientService.LogoutUserAsync(keycloakId, cancellationToken);

            return;
        }
    }
}
