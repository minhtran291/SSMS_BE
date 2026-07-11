using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.ProductImages;
using SSMS.Infrustructure.DatabaseConfig;
using SSMS.Infrustructure.Repositories.Base;

namespace SSMS.Infrustructure.Repositories.ProductImages
{
    public class ProductImageRepository(SSMSContext context) : Repository<ProductImage>(context), IProductImageRepository
    {
    }
}
