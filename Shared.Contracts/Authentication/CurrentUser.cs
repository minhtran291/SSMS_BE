namespace Shared.Contracts.Authentication
{
    public class CurrentUser
    {
        public string KeycloakUserId { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        //public IReadOnlyCollection<string> Roles { get; init; } = [];
    }
}
