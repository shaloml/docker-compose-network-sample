using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.DataAcsess.Core
{

    public abstract class AbstractUnitOfWork<TUnitOfWork, TDbContext> : IDisposable, IUnitOfWork
        where TUnitOfWork : AbstractUnitOfWork<TUnitOfWork, TDbContext>
        where TDbContext : AbstractDbContext<TDbContext>
    {
        private TDbContext _dbContext;
        private readonly ILogger<TUnitOfWork> _logger;

        public AbstractUnitOfWork(TDbContext dbContext, ILogger<TUnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public TDbContext DataContext
        {
            get
            {
                return _dbContext;
            }
        }

        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(DataContext);
        }

        public void Dispose()
        {

            if (_dbContext != null)
                _dbContext.Dispose();

            _dbContext = null;
        }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return DataContext.SaveChangesAsync();
        }

        #region  Transactions
        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {

            _logger.LogDebug("Begin Transaction Conn: {0}", _dbContext.GetHashCode());


            return DataContext.Database.BeginTransaction(isolationLevel);

        }

        public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {

            _logger.LogDebug("Begin Transaction Conn: {0}", _dbContext.GetHashCode());

            return DataContext.Database.BeginTransactionAsync(isolationLevel);
        }

        public TExecQuery CreateExecQuery<TExecQuery>() where TExecQuery : AbstractExecQuery, new()
        {
            TExecQuery execQuery = new TExecQuery();
            execQuery.Init(DataContext);

            return execQuery;
        }

        #endregion
    }
}
