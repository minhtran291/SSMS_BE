using SSMS.Domain.Entities;
using SSMS.Application.Repositories.Brands;
using SSMS.Infrastructure.DatabaseConfig;
using SSMS.Infrastructure.Repositories.Base;

namespace SSMS.Infrastructure.Repositories.Brands
{
    public class BrandRepository(SSMSContext context) : Repository<Brand>(context), IBrandRepository
    {
    }
}
