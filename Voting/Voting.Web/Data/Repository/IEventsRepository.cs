
namespace Voting.Web.Data
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;
    using Voting.Web.Models;

    public interface IEventsRepository : IGenericRepository<Events>
    {
        IQueryable GetEventWithCandidate();

        IQueryable GetEvent();
        IQueryable GetEventWithCandidateWithResult();

        Task<Events> GetEventsWithCandidateAsync(int id);

        Task<Candidate> GetCandidateAsync(int id);

        Task AddCandidateAsync(CandidateViewModel model, string path);

        Task<int> UpdateCandidateAsync(Candidate candidate);

        Task<int> DeleteCandidateAsync(Candidate candidate);



    }

}
