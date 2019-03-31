namespace Voting.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Entities;
    using System.Linq;

    public class DataContext : IdentityDbContext<User>
    {
       public DbSet<Events> Events { get; set; }
       public DbSet<Candidate> Candidates { get; set; }
     //  public DbSet<Vote> Votes { get; set; }
       public DbSet<Country> Countries { get; set; }
       public DbSet<City> Cities { get; set; }
        

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //To Disable Cascade Delete Rule
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
