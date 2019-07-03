using Microsoft.AspNetCore.Mvc;
using SportWebApi.Services;

namespace SportWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportMenuController : ControllerBase
    {
        private readonly SportService _sportService;

        public SportMenuController(SportService sportService)
        {
            _sportService = sportService;
        }

        // GET api/SportMenu
        [HttpGet]
        public JsonResult Get()
        {
            var result = _sportService.GetSportMenu();
            return new JsonResult(result);
        }

        // GET api/SportMenu/5
        [HttpGet("{clientId}")]
        public JsonResult Get(int clientId)
        {
            var result = _sportService.GetSportMenu(clientId);
            return new JsonResult(result);
        }
    }
}
