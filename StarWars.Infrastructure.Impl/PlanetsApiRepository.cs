using StarWars.Infrastructure.Contracts;
using StarWars.Infrastructure.Contracts.EntitiesApi;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StarWars.Infrastructure.Impl
{
    public class PlanetsApiRepository : IPlanetsApiRepository
    {
        public async Task<List<PlanetSWApiEntity>> GetAll()
        {
            using HttpClient client = new();


            HttpResponseMessage dataFromWebApi = await client.GetAsync("https://swapi.dev/api/planets/?format=json");
            string dataAsString = await dataFromWebApi.Content.ReadAsStringAsync();
            JsonSerializerOptions deserializerOptions = new()
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            PlanetListSWApiEntity? dataDeserialized = JsonSerializer.Deserialize<PlanetListSWApiEntity>(dataAsString, deserializerOptions);

            return dataDeserialized.Data ?? new List<PlanetSWApiEntity>();
        }
    }
}
