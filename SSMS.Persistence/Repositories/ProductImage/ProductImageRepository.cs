using SSMS.Persistence.DatabaseConfig;
using SSMS.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Persistence.Repositories.ProductImage
{
    public class ProductImageRepository(SSMSContext context) : Repository<Domain.Entities.ProductImage>(context), IProductImageRepository
    {
    }
}
