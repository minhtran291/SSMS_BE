using SSMS.Domain.Entities;
using SSMS.Application.Repositories.Base;

namespace SSMS.Application.Repositories.Products
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
