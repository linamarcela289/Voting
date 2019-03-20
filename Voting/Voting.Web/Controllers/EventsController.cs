namespace Voting.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
  

    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IUserHelper userHelper;


        public EventsController(IEventsRepository eventsRepository, IUserHelper userHelper)
        {
            this.eventsRepository = eventsRepository;
            this.userHelper = userHelper;

        }

        // GET: Events
        public IActionResult Index()
        {
            return View(this.eventsRepository.GetAll().OrderBy(e => e.Name));
        }


        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.eventsRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Events events)
        {
            if (ModelState.IsValid)
            {
              
                events.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await this.eventsRepository.CreateAsync(events);
                return RedirectToAction(nameof(Index));
            }

            return View(events);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.eventsRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Events events)
        {
            if (ModelState.IsValid)
            {
                try
                {
                
                    events.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await this.eventsRepository.UpdateAsync(events);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.eventsRepository.ExistAsync(events.Id))
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

            return View(events);
        }


        // GET: Events/Delete/5
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

            return View(events);
        }


        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.eventsRepository.GetByIdAsync(id);
            await this.eventsRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
