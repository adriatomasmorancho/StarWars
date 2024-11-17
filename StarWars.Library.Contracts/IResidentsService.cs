using StarWars.Library.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Library.Contracts
{
    public interface IResidentsService
    {
        Task<GetResidentsByPlanetNameRsDto> GetResidentsByPlanetName(string planetName);
    }
}
