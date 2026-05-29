using SSMS.Persistence.Repositories.Brand;
using SSMS.Persistence.Repositories.Category;
using SSMS.Persistence.Repositories.Product;
using SSMS.Persistence.Repositories.Size;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IBrandRepository Brand { get; }
        ISizeRepository Size { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
