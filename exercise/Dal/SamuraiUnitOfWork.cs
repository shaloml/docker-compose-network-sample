using Infrastructure.DataAcsess.Core;
using Microsoft.Extensions.Logging;

namespace ex4.Dal
{
    public class SamuraiUnitOfWork : AbstractUnitOfWork<SamuraiUnitOfWork, SamuraiDbContext>
    {
        public SamuraiUnitOfWork(SamuraiDbContext dbContext, ILogger<SamuraiUnitOfWork> logger) : base(dbContext, logger)
        {
        }
    }
}
