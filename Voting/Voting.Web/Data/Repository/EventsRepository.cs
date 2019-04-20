
namespace Voting.Web.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Voting.Web.Models;

    public class EventsRepository : GenericRepository<Events>, IEventsRepository
    {
        private readonly DataContext context;
        public EventsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
        public async Task AddCandidateAsync(CandidateViewModel model, string path)
        {
            var events = await this.GetEventsWithCandidateAsync(model.EventId);
            if (events == null)
            {
                return;
            }
            events.Candidates.Add(new Candidate
            {
                Name = model.Name,
                Proposal = model.Proposal,
                ImageUrl = path
            });
            this.context.Events.Update(events);
            await this.context.SaveChangesAsync();
        }

        public async Task<int> DeleteCandidateAsync(Candidate candidate)
        {
            var events = await this.context.Events.Where(c => c.Candidates.Any(ci => ci.Id == candidate.Id)).FirstOrDefaultAsync();
            if (events == null)
            {
                return 0;
            }

            this.context.Candidates.Remove(candidate);
            await this.context.SaveChangesAsync();
            return events.Id;
        }

        public IQueryable GetEventWithCandidate()
        {
            return this.context.Events
                .Include(c => c.Candidates)
                .Include(v => v.Vote)
                .OrderBy(c => c.Name);
        }

        public IQueryable GetEvent()
        {
            return this.context.Events
           .Include(c => c.Candidates)
           .OrderBy(c => c.Name);
        }
        public IQueryable GetEventWithCandidateWithResult()
        {
            return this.context.Events
                .Where(d => d.EndDate < DateTime.Now)
                .Include(c => c.Candidates)
                .Include(v => v.Vote)
                .OrderBy(c => c.Name);
        }

        public async Task<Events> GetEventsWithCandidateAsync(int id)
        {
            return await this.context.Events
            .Include(c => c.Candidates)
            .Include(v => v.Vote)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateCandidateAsync(Candidate candidate)
        {
            var events = await this.context.Events.Where(c => c.Candidates.Any(ci => ci.Id == candidate.Id)).FirstOrDefaultAsync();
            if (events == null)
            {
                return 0;
            }

            this.context.Candidates.Update(candidate);
            await this.context.SaveChangesAsync();
            return events.Id;
        }

        public async Task<Candidate> GetCandidateAsync(int id)
        {
            return await this.context.Candidates.FindAsync(id);
        }
    }
}
