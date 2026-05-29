using SSMS.Persistence.DatabaseConfig;
using SSMS.Persistence.Repositories.Base;

namespace SSMS.Persistence.Repositories.Brand
{
    public class BrandRepository(SSMSContext context) : Repository<Domain.Entities.Brand>(context), IBrandRepository
    {
    }
}
