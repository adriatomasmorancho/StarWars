using StarWars.Library.Contracts.DTOs;
using System.Xml.Linq;
using System;

namespace StarWars.Library.Contracts
{
    public interface IPlanetsService
    {

        Task<RefreshAndListPlanetNamesRsDto> RefreshPlanets();
        Task<CreatePlanetRsDto> CreatePlanets(string name, int rotation, int period, string climate, string poblation, string url);

    }
}
