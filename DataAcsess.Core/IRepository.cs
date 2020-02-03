using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.DataAcsess.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Count();
        void Delete(TEntity entity);
        void DeleteRange(Expression<Func<TEntity, bool>> predicate = null);
        int ExecQuery(FormattableString query);
        IQueryable<TEntity> FetchAll();
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> FindWithPaging<KProperty>(int pageIndex, int pageSize, out int count, Expression<Func<TEntity, bool>> predicateExpression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        TEntity GetById(params object[] keyValues);
        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Reload(TEntity entity);
        IQueryable<TEntity> SelectQuery(FormattableString query);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        ValueTask<TEntity> GetByIdAsync(params object[] keyValues);
        ValueTask<TEntity> GetByIdAsync(object[] keyValues, CancellationToken cancellationToken);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task InsertRangeAsync(IEnumerable<TEntity> entities);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<long> LongCountAsync(CancellationToken cancellationToken = default);
        Task<int> ExecQueryAsync(FormattableString query);
        Task<List<TEntity>> FindWithPagingAsync<KProperty>(int pageIndex, int pageSize, out int count, Expression<Func<TEntity, bool>> predicateExpression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true);
        Task ReloadAsync(TEntity entity);
    }
}