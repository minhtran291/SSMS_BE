using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.Products;
using SSMS.Infrustructure.DatabaseConfig;
using SSMS.Infrustructure.Repositories.Base;

namespace SSMS.Infrustructure.Repositories.Products
{
    public class ProductRepository(SSMSContext context) : Repository<Product>(context), IProductRepository
    {
    }
}
