using SSMS.Persistence.DatabaseConfig;
using SSMS.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Persistence.Repositories.ProductSizePrice
{
    public class ProductSizePriceRepository(SSMSContext context) : Repository<Domain.Entities.ProductSizePrice>(context), IProductSizePriceRepository
    {
    }
}
