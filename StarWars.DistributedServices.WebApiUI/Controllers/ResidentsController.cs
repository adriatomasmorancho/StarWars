using Microsoft.AspNetCore.Mvc;
using StarWars.Library.Contracts;
using StarWars.Library.Contracts.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarWars.DistributedServices.WebApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentsController : ControllerBase
    {
        private readonly IResidentsService _residentsService;

        public ResidentsController(IResidentsService residentsService)
        {
            _residentsService = residentsService;
        }

        [HttpGet("GetResidentsByPlanetName")]
        public async Task<IActionResult> GetResidentsByPlanetName(string planetName)
        {
            GetResidentsByPlanetNameRsDto result = await _residentsService.GetResidentsByPlanetName(planetName);
            if (result != null && result.data != null)
            {
                return Ok(result.data);
            }
            if (result != null && result.errors != null)
            {
                return BadRequest(result.errors);
            }

            return BadRequest("unknown error");
        }

    }
}
