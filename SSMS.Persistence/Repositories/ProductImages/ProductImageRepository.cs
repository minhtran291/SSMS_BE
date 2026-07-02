using SSMS.Domain.Repositories.ProductImages;
using SSMS.Infrustructure.DatabaseConfig;
using SSMS.Infrustructure.Repositories.Base;

namespace SSMS.Infrustructure.Repositories.ProductImages
{
    public class ProductImageRepository(SSMSContext context) : Repository<Domain.Entities.ProductImage>(context), IProductImageRepository
    {
    }
}
