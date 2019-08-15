namespace Voting.Web.Controllers.API
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using Voting.Web.Data.Entities;
    using Voting.Web.Helpers;

    [Route("api/[Controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventsController : Controller
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IUserHelper userHelper;

        public EventsController(IEventsRepository eventsRepository,
            IUserHelper userHelper)
        {
            this.eventsRepository = eventsRepository;
            this.userHelper = userHelper;
        }
        [HttpGet]
        public IActionResult GetEvents()
        {
            return this.Ok(this.eventsRepository.GetAllWithUsers());
        }
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody]  Events events)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(events.User.Email);
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }
            var entityEvent = new Events
            {
                Name = events.Name,
                Decription = events.Decription,
                StarDate = events.StarDate,
                EndDate = events.EndDate,
                User = user
            };

            var newEvents= await this.eventsRepository.CreateAsync(entityEvent);
            return Ok(newEvents);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Events events)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != events.Id)
            {
                return BadRequest();
            }

            var oldEvents = await this.eventsRepository.GetByIdAsync(id);
            if (oldEvents == null)
            {
                return this.BadRequest("Product Id don't exists.");
            }

            oldEvents.Name = events.Name;
            oldEvents.Decription = events.Decription;
            oldEvents.StarDate = events.StarDate;
            oldEvents.EndDate = events.EndDate;

            var updatedProduct = await this.eventsRepository.UpdateAsync(oldEvents);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var product = await this.eventsRepository.GetByIdAsync(id);
            if (product == null)
            {
                return this.NotFound();
            }

            await this.eventsRepository.DeleteAsync(product);
            return Ok(product);
        }

    }
}
