using SSMS.Domain.Entities;
using SSMS.Application.Repositories.ProductSizePrices;
using SSMS.Infrastructure.DatabaseConfig;
using SSMS.Infrastructure.Repositories.Base;

namespace SSMS.Infrastructure.Repositories.ProductSizePrices
{
    public class ProductSizePriceRepository(SSMSContext context) : Repository<ProductSizePrice>(context), IProductSizePriceRepository
    {
    }
}
