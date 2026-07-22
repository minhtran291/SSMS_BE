using SSMS.Domain.Entities;
using SSMS.Application.Repositories.Base;

namespace SSMS.Application.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
