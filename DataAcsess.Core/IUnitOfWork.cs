using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.DataAcsess.Core
{
    public interface IUnitOfWork
    {
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;

        void SaveChanges();
        Task SaveChangesAsync();

        TExecQuery CreateExecQuery<TExecQuery>() where TExecQuery : AbstractExecQuery, new();



    }
}
