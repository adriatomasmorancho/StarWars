using Microsoft.AspNetCore.Mvc;
using StarWars.Library.Contracts;
using StarWars.Library.Contracts.DTOs;
using StarWars.XCutting.Enums;

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

        [HttpGet("RefreshAndListPlanetNames")]
        public async Task<IActionResult> RefreshAndListPlanetNames()
        {
            RefreshAndListPlanetNamesRsDto result = await _planetsService.RefreshPlanets();
            if (result.errors != null)
            {
                return BadRequest(result.errors.Select(MapErrorEnumToString));
            }
            return Ok(result.data);
        }

        private static string MapErrorEnumToString(RefreshAndListPlanetNamesErrorEnum errorEnum)
        {
            return errorEnum switch
            {
                RefreshAndListPlanetNamesErrorEnum.SWApiErrorConnection => "Error while connecting to SWApi",
                RefreshAndListPlanetNamesErrorEnum.EntityMappingConnection => "Error while mapping SWApi data to save it in database",
                RefreshAndListPlanetNamesErrorEnum.SWDbErrorConnection => "Error while connecting to SWDB",
                _ => "unknown error",
            };
        }

        [HttpPost("CreatePlanet")]
        public async Task<IActionResult> Create(string name, int rotation, int period, string climate, string poblation, string url)
        {

            CreatePlanetRsDto result = await _planetsService.CreatePlanets(name, rotation, period, climate, poblation, url);

            return Ok(result);
        }


    }
}
