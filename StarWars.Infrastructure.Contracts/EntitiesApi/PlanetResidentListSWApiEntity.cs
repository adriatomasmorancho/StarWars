using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Contracts.EntitiesApi
{
    public class PlanetResidentListSWApiEntity
    {
        [JsonPropertyName("residents")]
        public List<string> ResidentUrlList { get; set; } = new();
    }
}
