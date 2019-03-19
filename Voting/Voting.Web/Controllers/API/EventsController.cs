namespace Voting.Web.Controllers.API
{
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    public class EventsController : Controller
    {
        private readonly IEventsRepository eventsRepository;

        public EventsController(IEventsRepository eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }
        [HttpGet]
        public IActionResult GetEvents()
        {
            return this.Ok(this.eventsRepository.GetAllWithUsers());
        }

    }
}
