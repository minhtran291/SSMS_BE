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
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}