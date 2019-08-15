namespace Voting.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Entities;
    using System.Linq;
    using LeasingHome.Web.Data.Entities;

    public class DataContext : IdentityDbContext<User>
    {
       public DbSet<Events> Events { get; set; }
       public DbSet<Candidate> Candidates { get; set; }
       public DbSet<Vote> Votes { get; set; }
       public DbSet<Country> Countries { get; set; }
       public DbSet<City> Cities { get; set; }


       public DbSet<Contract> Contracts { get; set; }

       public DbSet<Lessee> Lessees { get; set; }

       public DbSet<Owner> Owners { get; set; }

       public DbSet<Property> Properties { get; set; }

       public DbSet<PropertyImage> PropertyImages { get; set; }

       public DbSet<PropertyType> PropertyTypes { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                .G­etEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
