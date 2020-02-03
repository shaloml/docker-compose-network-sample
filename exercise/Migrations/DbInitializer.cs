using ex4.Dal;
using ex4.Dal.Model;
using System.Linq;

namespace Exercise.Migrations
{
    public static class DbInitializer
    {
        public static void Initialize(SamuraiDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Samurais.Any())
            {
                return;   // DB has been seeded
            }

            var samurais = new Samurai[]
            {
                new Samurai(){Age="44",Killing=32,Name="Moshe", SecretIdentity= new SecretIdentity{RealName="Rubin" } },
                new Samurai(){Age="21",Killing=32,Name="Omer", SecretIdentity= new SecretIdentity{RealName="Dison" } },
                new Samurai(){Age="54",Killing=32,Name="Ruti", SecretIdentity= new SecretIdentity{RealName="Blue" } }
            };

            context.Samurais.AddRange(samurais);
            context.SaveChanges();

            context.SaveChanges();
        }
    }
}
