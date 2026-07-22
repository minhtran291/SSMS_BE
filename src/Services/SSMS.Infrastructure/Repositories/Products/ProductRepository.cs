using SSMS.Domain.Entities;
using SSMS.Application.Repositories.Products;
using SSMS.Infrastructure.DatabaseConfig;
using SSMS.Infrastructure.Repositories.Base;

namespace SSMS.Infrastructure.Repositories.Products
{
    public class ProductRepository(SSMSContext context) : Repository<Product>(context), IProductRepository
    {
    }
}
