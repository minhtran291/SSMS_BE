using Microsoft.EntityFrameworkCore.Storage;
using SSMS.Persistence.DatabaseConfig;
using SSMS.Persistence.Repositories.Brand;
using SSMS.Persistence.Repositories.Category;
using SSMS.Persistence.Repositories.Product;
using SSMS.Persistence.Repositories.ProductImage;
using SSMS.Persistence.Repositories.ProductSizePrice;
using SSMS.Persistence.Repositories.Size;

namespace SSMS.Persistence.UnitOfWork
{
    public class UnitOfWork(SSMSContext context,
        IProductRepository product, 
        ICategoryRepository category, 
        IBrandRepository brand, 
        ISizeRepository size, 
        IProductSizePriceRepository productSizePrice, 
        IProductImageRepository productImage) : IUnitOfWork
    {
        private readonly SSMSContext _context = context;
        private IDbContextTransaction? _transaction;

        public IProductRepository Product { get; private set; } = product;
        public ICategoryRepository Category { get; private set; } = category;
        public IBrandRepository Brand { get; private set; } = brand;
        public ISizeRepository Size { get; private set; } = size;
        public IProductSizePriceRepository ProductSizePrice { get; private set; } = productSizePrice;
        public IProductImageRepository ProductImage { get; private set; } = productImage;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction ??= await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                var transaction = _transaction;
                _transaction = null;
                try
                {
                    await transaction.CommitAsync();
                }
                finally
                {
                    await transaction.DisposeAsync();
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                var transaction = _transaction;
                _transaction = null;

                try
                {
                    await transaction.RollbackAsync();

                }
                finally
                {
                    await transaction.DisposeAsync();
                }
            }
        }
    }
}