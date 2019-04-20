namespace Voting.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Voting.Web.Models;  

   
    public class EventsController : Controller
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IUserHelper userHelper;

        public EventsController(IEventsRepository eventsRepository, IUserHelper userHelper)
        {
            this.eventsRepository = eventsRepository;
            this.userHelper = userHelper;
        }
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

            var view = this.ToCandidateViewModel(candidate);
            return View(view);
        }

        private CandidateViewModel ToCandidateViewModel(Candidate candidate)
        {
            return new CandidateViewModel
            {
                Id = candidate.Id,
                ImageUrl = candidate.ImageUrl,
                Proposal = candidate.Proposal,
                Name = candidate.Name
            };
        }

        [HttpPost]
        public async Task<IActionResult> EditCandidate(CandidateViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = view.ImageUrl;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(), 
                        "wwwroot\\images\\Candidates", 
                        view.ImageFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }
                        path = $"~/images/Candidates/{view.ImageFile.FileName}";
                    }

                    var candidate = this.ToCandidate(view, path);
                    await this.eventsRepository.UpdateCandidateAsync(candidate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.eventsRepository.ExistAsync(view.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(view);

        }

        private Candidate ToCandidate(CandidateViewModel view, string path)
        {
            return new Candidate
            {
                Id = view.Id,
                ImageUrl = path,
                Proposal = view.Proposal,
                Name = view.Name
            };
        }

        [Authorize(Roles = "Admin")]
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
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                       "wwwroot\\images\\Candidates",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Candidates/{file}";
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

        public IActionResult Result()
        {
            return View(this.eventsRepository.GetEventWithCandidateWithResult());
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

        public async Task<IActionResult> DetailsResult(int? id)
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
           // var view = this.ToProducViewModel(product);

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

        [Authorize(Roles = "Admin")]
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
