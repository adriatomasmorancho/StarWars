using StarWars.Infrastructure.Contracts;
using StarWars.Infrastructure.Contracts.EntitiesApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Impl
{
    public class PeopleApiRepository : IPeopleApiRepository
    {
        public async Task<PeopleSWApiEntity?> TryGetByUrl(string url)
        {
            using HttpClient client = new();

            HttpResponseMessage dataFromWebApi = await client.GetAsync(url);
            string dataAsString = await dataFromWebApi.Content.ReadAsStringAsync();

            JsonSerializerOptions deserializerOptions = new()
            {
                PropertyNameCaseInsensitive = true
            };
            PeopleSWApiEntity? dataDeserialized = JsonSerializer.Deserialize<PeopleSWApiEntity?>(dataAsString, deserializerOptions);

            return dataDeserialized;
        }
    }
}
