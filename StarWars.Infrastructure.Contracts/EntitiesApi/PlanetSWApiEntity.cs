using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Contracts.EntitiesApi
{
    public class PlanetSWApiEntity
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rotation_period")]
        public string RotationPeriod { get; set; }

        [JsonPropertyName("orbital_period")]
        public string OrbitalPeriod { get; set; }

        [JsonPropertyName("climate")]
        public string Climate { get; set; }

        [JsonPropertyName("population")]
        public string Population { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
        

    }
}
