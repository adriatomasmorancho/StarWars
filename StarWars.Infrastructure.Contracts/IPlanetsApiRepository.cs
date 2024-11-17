using StarWars.Infrastructure.Contracts.EntitiesApi;

namespace StarWars.Infrastructure.Contracts
{
    public interface IPlanetsApiRepository
    {
        Task<List<PlanetSWApiEntity>> GetAll();

        Task<PlanetResidentListSWApiEntity?> TryGetPlanetResidentListByPlanetUrl(string url);

    }
}
