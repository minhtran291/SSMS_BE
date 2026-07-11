using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.Sizes;
using SSMS.Infrustructure.DatabaseConfig;
using SSMS.Infrustructure.Repositories.Base;

namespace SSMS.Infrustructure.Repositories.Sizes
{
    public class SizeRepository(SSMSContext context) : Repository<Size>(context), ISizeRepository
    {
    }
}
