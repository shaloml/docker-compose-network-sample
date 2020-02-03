using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.DataAcsess.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext m_DbContext;
        private readonly DbSet<TEntity> m_DbSet;

        public Repository(DbContext dbContext)
        {
            m_DbContext = dbContext;
            m_DbSet = dbContext.Set<TEntity>();
        }

        private DatabaseFacade Database
        {
            get
            {
                return m_DbContext.Database;
            }
        }

        public int Count()
        {
            return m_DbSet.Count();
        }

        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return m_DbSet.CountAsync(cancellationToken);
        }

        public long LongCount()
        {
            return m_DbSet.Count();
        }

        public Task<long> LongCountAsync(CancellationToken cancellationToken = default)
        {
            return m_DbSet.LongCountAsync(cancellationToken);
        }

        public TEntity GetById(params object[] keyValues)
        {
            if (keyValues == null)
                throw new ArgumentNullException("keyValues");

            return m_DbSet.Find(keyValues);
        }

        public ValueTask<TEntity> GetByIdAsync(params object[] keyValues)
        {
            if (keyValues == null)
                throw new ArgumentNullException("keyValues");

            return m_DbSet.FindAsync(keyValues);
        }

        public ValueTask<TEntity> GetByIdAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            if (keyValues == null)
                throw new ArgumentNullException("keyValues");

            return m_DbSet.FindAsync(keyValues, cancellationToken);
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            //When the entity is null, there is no access to it's properties        
            return m_DbSet.Where(predicate).FirstOrDefault();
        }
        public Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            //When the entity is null, there is no access to it's properties        
            return m_DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return m_DbSet.Where(predicate).AsQueryable<TEntity>().ToListAsync();

            return m_DbSet.AsQueryable<TEntity>().ToListAsync();
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return m_DbSet.Where(predicate).AsQueryable<TEntity>();

            return m_DbSet.AsQueryable<TEntity>();
        }

        public virtual IQueryable<TEntity> FetchAll()
        {
            return m_DbSet;
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            m_DbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            m_DbSet.UpdateRange(entities);

        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            m_DbSet.Add(entity);
        }
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            return m_DbSet.AddAsync(entity, cancellationToken).AsTask();
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            m_DbSet.AddRange(entities);
        }
        public Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            return m_DbSet.AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            var entry = m_DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                m_DbSet.Attach(entity);
            }
            m_DbSet.Remove(entity);
        }

        public void DeleteRange(Expression<Func<TEntity, bool>> predicate = null)
        {
            IEnumerable<TEntity> list = FindAll(predicate);

            foreach (var entity in list)
            {
                if (m_DbContext.Entry(entity).State == EntityState.Detached)
                {
                    m_DbSet.Attach(entity);
                }
            }
            m_DbSet.RemoveRange(list);
        }


        public IQueryable<TEntity> SelectQuery(FormattableString query)
        {
            return m_DbSet.FromSqlInterpolated(query);
        }

        public int ExecQuery(FormattableString query)
        {
            return this.Database.ExecuteSqlInterpolated(query);
        }

        public Task<int> ExecQueryAsync(FormattableString query)
        {
            return this.Database.ExecuteSqlInterpolatedAsync(query);
        }

        public IQueryable<TEntity> FindWithPaging<KProperty>(int pageIndex, int pageSize, out int count, Expression<Func<TEntity, bool>> predicateExpression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            var query = (predicateExpression == null) ? m_DbSet : m_DbSet.Where(predicateExpression);
            count = query.Count();
            IQueryable<TEntity> res;
            if (orderByExpression == null)
            {
                res = query.Skip(pageSize * pageIndex).Take(pageSize);
            }
            if (ascending)
            {
                res = query.OrderBy(orderByExpression).Skip(pageSize * pageIndex).Take(pageSize);
            }
            else
            {
                res = query.OrderByDescending(orderByExpression).Skip(pageSize * pageIndex).Take(pageSize);
            }
            return res;
        }

        public Task<List<TEntity>> FindWithPagingAsync<KProperty>(int pageIndex, int pageSize, out int count, Expression<Func<TEntity, bool>> predicateExpression, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending = true)
        {
            var query = (predicateExpression == null) ? m_DbSet : m_DbSet.Where(predicateExpression);
            count = query.Count();
            IQueryable<TEntity> res;
            if (orderByExpression == null)
            {
                res = query.Skip(pageSize * pageIndex).Take(pageSize);
            }
            if (ascending)
            {
                res = query.OrderBy(orderByExpression).Skip(pageSize * pageIndex).Take(pageSize);
            }
            else
            {
                res = query.OrderByDescending(orderByExpression).Skip(pageSize * pageIndex).Take(pageSize);
            }
            return res.ToListAsync();
        }

        public void Reload(TEntity entity)
        {
            m_DbContext.Entry(entity).Reload();
        }

        public Task ReloadAsync(TEntity entity)
        {
            return m_DbContext.Entry(entity).ReloadAsync();
        }
    }
}
