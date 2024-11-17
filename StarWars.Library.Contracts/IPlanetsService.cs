using StarWars.Library.Contracts.DTOs;

namespace StarWars.Library.Contracts
{
    public interface IPlanetsService
    {

        Task<RefreshAndListPlanetNamesRsDto> RefreshPlanets();

    }
}
