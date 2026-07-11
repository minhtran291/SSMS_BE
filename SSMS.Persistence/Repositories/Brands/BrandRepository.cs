using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.Brands;
using SSMS.Infrustructure.DatabaseConfig;
using SSMS.Infrustructure.Repositories.Base;

namespace SSMS.Infrustructure.Repositories.Brands
{
    public class BrandRepository(SSMSContext context) : Repository<Brand>(context), IBrandRepository
    {
    }
}
