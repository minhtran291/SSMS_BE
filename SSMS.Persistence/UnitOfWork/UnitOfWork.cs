using Microsoft.EntityFrameworkCore.Storage;
using SSMS.Persistence.DatabaseConfig;
using SSMS.Persistence.Repositories.Product;

namespace SSMS.Persistence.UnitOfWork
{
    public class UnitOfWork(SSMSContext context,
        IProductRepository product) : IUnitOfWork
    {
        private readonly SSMSContext _context = context;
        private IDbContextTransaction? _transaction;

        public IProductRepository Product { get; private set; } = product;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction ??= await _context.Database.BeginTransactionAsync();
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