using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DataAcsess.Core
{
    public abstract class AbstractDbContext<TDBContext> : DbContext where TDBContext : AbstractDbContext<TDBContext>
    {
        private readonly ILoggerFactory _loggerFactory;


        //public static readonly ILoggerFactory consoleLoggerFactory
        // = new LoggerFactory().AddConsole();

        //public static readonly ILoggerFactory _consoleLoggerFactory
        //    = new LoggerFactory(new[] {
        //          new ConsoleLoggerProvider((category, level) =>
        //            category == "EF:" + DbLoggerCategory.Database.Command.Name &&
        //            level == LogLevel.Information, true)
        //        });


        //public AbstractDbContext(DbContextOptions<TDBContext> options) : base(options)
        //{
        //    _loggerFactory = null;

        //}
        public AbstractDbContext(DbContextOptions<TDBContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            //if (_loggerFactory != null)
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            //else
            //    optionsBuilder.UseLoggerFactory(_consoleLoggerFactory);

            optionsBuilder.EnableSensitiveDataLogging();
        }


    }
}
