using ex4.Dal.Model;
using Infrastructure.DataAcsess.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ex4.Dal
{
    public class SamuraiDbContext : AbstractDbContext<SamuraiDbContext>
    {
        public SamuraiDbContext(DbContextOptions<SamuraiDbContext> options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
        {
        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<SecretIdentity> SecretIdentity { get; set; }


        protected override void OnModelCreating
            (ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer(
            //     "Data Source=localhost;Initial Catalog=dddddd;Integrated Security=True");
        }

    }
}
