namespace Voting.Web.Controllers
{
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Threading.Tasks;
    using Voting.Web.Models;

    [Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IUserHelper userHelper;

        public EventsController(IEventsRepository eventsRepository, IUserHelper userHelper)
        {
            this.eventsRepository = eventsRepository;
            this.userHelper = userHelper;
        }

        public async Task<IActionResult> DeleteCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.eventsRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            var eventId = await this.eventsRepository.DeleteCandidateAsync(candidate);
            return this.RedirectToAction($"Details/{eventId}");
        }

        public async Task<IActionResult> EditCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.eventsRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> EditCandidate(CandidateViewModel candidate)
        {
            if (this.ModelState.IsValid)
            {
                var eventsId = await this.eventsRepository.UpdateCandidateAsync(candidate);
                if (eventsId != 0)
                {
                    return this.RedirectToAction($"Details/{eventsId}");
                }
            }

            return this.View(candidate);
        }

        public async Task<IActionResult> AddCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.eventsRepository.GetByIdAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            var model = new CandidateViewModel { EventId = country.Id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate(CandidateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Candidates", model.ImageFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Candidates/{model.ImageFile.FileName}";
                }

                await this.eventsRepository.AddCandidateAsync(model, path);
                return this.RedirectToAction($"Details/{model.EventId}");

            }

            return this.View(model);
        }

        public IActionResult Index()
        {
            return View(this.eventsRepository.GetEventWithCandidate());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await this.eventsRepository.GetEventsWithCandidateAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Events events)
        {
            if (ModelState.IsValid)
            {
                await this.eventsRepository.CreateAsync(events);
                return RedirectToAction(nameof(Index));
            }

            return View(events);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await this.eventsRepository.GetByIdAsync(id.Value);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Events events)
        {
            if (ModelState.IsValid)
            {
                await this.eventsRepository.UpdateAsync(events);
                return RedirectToAction(nameof(Index));
            }

            return View(events);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await this.eventsRepository.GetByIdAsync(id.Value);
            if (events == null)
            {
                return NotFound();
            }

            await this.eventsRepository.DeleteAsync(events);
            return RedirectToAction(nameof(Index));
        }

    }
}
