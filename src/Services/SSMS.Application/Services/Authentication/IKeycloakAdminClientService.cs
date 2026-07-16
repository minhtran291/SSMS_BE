namespace SSMS.Application.Services.Authentication
{
    public interface IKeycloakAdminClientService
    {
        Task LogoutUserAsync(string keycloakUserId, CancellationToken cancellationToken = default);
    }
}
