
namespace Voting.Web.Data
{
    using Entities;
    public class EventsRepository : GenericRepository<Events>, IEventsRepository
    {
        public EventsRepository(DataContext context): base(context)
        {
        }
    }
}
