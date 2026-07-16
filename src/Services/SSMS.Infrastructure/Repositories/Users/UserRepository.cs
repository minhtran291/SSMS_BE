using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.Users;
using SSMS.Infrastructure.DatabaseConfig;
using SSMS.Infrastructure.Repositories.Base;

namespace SSMS.Infrastructure.Repositories.Users
{
    public class UserRepository(SSMSContext context) : Repository<User>(context), IUserRepository
    {
    }
}
