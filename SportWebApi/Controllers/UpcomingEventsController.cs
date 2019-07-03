using Microsoft.AspNetCore.Mvc;
using SportWebApi.Services;

namespace SportWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpcomingEventsController : ControllerBase
    {
        private readonly SportService _sportService;

        public UpcomingEventsController(SportService sportService)
        {
            _sportService = sportService;
        }

        // GET api/UpcomingEvents
        [HttpGet]
        public JsonResult Get()
        {
            var result = _sportService.GetUpcomingEvents();
            return new JsonResult(result);
        }

        // GET api/UpcomingEvents/5
        [HttpGet("{clientId}")]
        public JsonResult Get(int clientId)
        {
            var result = _sportService.GetUpcomingEvents(clientId);
            return new JsonResult(result);
        }
    }
}
