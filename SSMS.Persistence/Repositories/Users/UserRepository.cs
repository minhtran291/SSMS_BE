using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.Users;
using SSMS.Infrustructure.DatabaseConfig;
using SSMS.Infrustructure.Repositories.Base;

namespace SSMS.Infrustructure.Repositories.Users
{
    public class UserRepository(SSMSContext context) : Repository<User>(context), IUserRepository
    {
    }
}
