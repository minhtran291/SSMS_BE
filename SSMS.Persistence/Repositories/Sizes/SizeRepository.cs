using SSMS.Domain.Repositories.Sizes;
using SSMS.Infrustructure.DatabaseConfig;
using SSMS.Infrustructure.Repositories.Base;

namespace SSMS.Infrustructure.Repositories.Sizes
{
    public class SizeRepository(SSMSContext context) : Repository<Domain.Entities.Size>(context), ISizeRepository
    {
    }
}
