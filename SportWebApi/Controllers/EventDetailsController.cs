using Microsoft.AspNetCore.Mvc;
using SportWebApi.Services;

namespace SportWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDetailsController : ControllerBase
    {
        private readonly SportService _sportService;

        public EventDetailsController(SportService sportService)
        {
            _sportService = sportService;
        }

        // GET api/EventDetails
        [HttpGet]
        public JsonResult Get()
        {
            var result = _sportService.GetEventDetails();
            return new JsonResult(result);
        }

        // GET api/EventDetails/5
        [HttpGet("{clientId}")]
        public JsonResult Get(int clientId)
        {
            var result = _sportService.GetEventDetails(clientId);
            return new JsonResult(result);
        }
    }
}
