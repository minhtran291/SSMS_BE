using Shared.Contracts.Authentication;

namespace SSMS.Application.Services.User
{
    public interface ICurrentUserService
    {
        CurrentUser? User { get; }
    }
}
