using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.Sizes;
using SSMS.Infrastructure.DatabaseConfig;
using SSMS.Infrastructure.Repositories.Base;

namespace SSMS.Infrastructure.Repositories.Sizes
{
    public class SizeRepository(SSMSContext context) : Repository<Size>(context), ISizeRepository
    {
    }
}
