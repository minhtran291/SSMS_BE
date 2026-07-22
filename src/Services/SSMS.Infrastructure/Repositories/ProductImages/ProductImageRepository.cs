using SSMS.Domain.Entities;
using SSMS.Application.Repositories.ProductImages;
using SSMS.Infrastructure.DatabaseConfig;
using SSMS.Infrastructure.Repositories.Base;

namespace SSMS.Infrastructure.Repositories.ProductImages
{
    public class ProductImageRepository(SSMSContext context) : Repository<ProductImage>(context), IProductImageRepository
    {
    }
}
