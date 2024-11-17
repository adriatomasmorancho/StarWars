using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static StarWars.Infrastructure.Contracts.EntitiesApi.PlanetSWApiEntity;

namespace StarWars.Infrastructure.Contracts.EntitiesApi
{
    public class PlanetListSWApiEntity
    {
        [JsonPropertyName("results")]
        public List<PlanetSWApiEntity> Data { get; set; } = new List<PlanetSWApiEntity>();

    }
}
