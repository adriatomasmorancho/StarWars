using Microsoft.AspNetCore.Mvc;
using StarWars.Library.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarWars.DistributedServices.WebApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetsService _planetsService;

        public PlanetsController(IPlanetsService planetsService)
        {
            _planetsService = planetsService;
        }

    
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
    }
}
