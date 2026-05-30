using SSMS.Persistence.Repositories.Brand;
using SSMS.Persistence.Repositories.Category;
using SSMS.Persistence.Repositories.Product;
using SSMS.Persistence.Repositories.ProductImage;
using SSMS.Persistence.Repositories.ProductSizePrice;
using SSMS.Persistence.Repositories.Size;

namespace SSMS.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IBrandRepository Brand { get; }
        ISizeRepository Size { get; }
        IProductSizePriceRepository ProductSizePrice { get; }
        IProductImageRepository ProductImage { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
