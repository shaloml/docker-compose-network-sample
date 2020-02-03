using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.DataAcsess.Core
{
    public abstract class AbstractExecQuery
    {
        private DbContext _dbContext = null;

        public void Init(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        protected TEntity ExecStoredProcedure<TEntity>(FormattableString procedureNameWithParameters) where TEntity : class
        {
            return _dbContext.ExecStoredProcedure<TEntity>(procedureNameWithParameters);
        }

        protected IEnumerable<TEntity> ExecStoredProcedureList<TEntity>(FormattableString procedureNameWithParameters) where TEntity : class
        {
            return _dbContext.ExecStoredProcedureList<TEntity>(procedureNameWithParameters);
        }

    }
}
