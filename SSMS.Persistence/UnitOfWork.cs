using Microsoft.EntityFrameworkCore.Storage;
using SSMS.Domain;
using SSMS.Infrustructure.DatabaseConfig;

namespace SSMS.Infrustructure
{
    public class UnitOfWork(SSMSContext context) : IUnitOfWork
    {
        private readonly SSMSContext _context = context;
        private IDbContextTransaction? _transaction;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is not null)
                throw new InvalidOperationException("Transaction đã được bắt đầu.");

            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is null)
                throw new InvalidOperationException("Transaction chưa được bắt đầu.");

            var transaction = _transaction;
            _transaction = null;

            try
            {
                await transaction.CommitAsync(cancellationToken);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is null)
                throw new InvalidOperationException("Transaction chưa được bắt đầu.");

            var transaction = _transaction;
            _transaction = null;

            try
            {
                await transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            var transaction = _transaction;
            _transaction = null;

            if (transaction is not null)
                await transaction.DisposeAsync();

            await _context.DisposeAsync();
        }
    }
}

