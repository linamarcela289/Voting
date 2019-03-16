namespace Voting.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Voting.Web.Data.Entities;

    public class DataContext : DbContext
    {
       public DbSet<Events> Events { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
