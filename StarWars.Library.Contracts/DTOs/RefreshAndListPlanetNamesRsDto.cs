using StarWars.XCutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Library.Contracts.DTOs
{
    public class RefreshAndListPlanetNamesRsDto
    {
        public List<RefreshAndListPlanetNamesErrorEnum>? errors;
        public List<string>? data;
    }
}
