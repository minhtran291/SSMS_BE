using SSMS.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Persistence.Repositories.Product
{
    public interface IProductRepository : IRepository<Domain.Entities.Product>
    {
    }
}
