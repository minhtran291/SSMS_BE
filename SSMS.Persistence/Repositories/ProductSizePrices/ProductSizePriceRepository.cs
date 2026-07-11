using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.ProductSizePrices;
using SSMS.Infrustructure.DatabaseConfig;
using SSMS.Infrustructure.Repositories.Base;

namespace SSMS.Infrustructure.Repositories.ProductSizePrices
{
    public class ProductSizePriceRepository(SSMSContext context) : Repository<ProductSizePrice>(context), IProductSizePriceRepository
    {
    }
}
