using StarWars.Infrastructure.Contracts.EntitiesApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Contracts
{
    public interface IPeopleApiRepository
    {
        Task<PeopleSWApiEntity?> TryGetByUrl(string url);
    }
}
