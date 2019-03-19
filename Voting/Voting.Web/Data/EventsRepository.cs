
namespace Voting.Web.Data
{
    using System.Linq;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class EventsRepository : GenericRepository<Events>, IEventsRepository
    {
        private readonly DataContext context;

        public EventsRepository(DataContext context): base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Events.Include(e =>e.User);
        }
    }
}
