using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Library.Contracts.DTOs
{
    public class CreatePlanetRsDto
    {
        public string? NombrePlaneta { get; set; }
        public int? RotacionOrbitalEnDiasAlderaanos { get; set; }
        public int? PeriodoOrbitalEnHorasAlderaanas { get; set; }
        public string? Clima { get; set; }
        public string? Poblacion { get; set; }
        public string? Url { get; set; }
    }
}
