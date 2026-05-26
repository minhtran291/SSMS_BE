using SSMS.Persistence.DatabaseConfig;
using SSMS.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Persistence.Repositories.Product
{
    public class ProductRepository(SSMSContext context) : Repository<Domain.Entities.Product>(context), IProductRepository
    {
    }
}
