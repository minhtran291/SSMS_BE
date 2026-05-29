using SSMS.Persistence.DatabaseConfig;
using SSMS.Persistence.Repositories.Base;

namespace SSMS.Persistence.Repositories.Size
{
    public class SizeRepository(SSMSContext context) : Repository<Domain.Entities.Size>(context), ISizeRepository
    {
    }
}
