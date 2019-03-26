
namespace Voting.Web.Data
{
    using Entities;
    using System.Linq;

    public interface IEventsRepository : IGenericRepository<Events>
    {
        IQueryable GetAllWithUsers();

    }

}
