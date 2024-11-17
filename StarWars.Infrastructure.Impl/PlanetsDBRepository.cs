using Microsoft.EntityFrameworkCore;
using StarWars.Infrastructure.Contracts;
using StarWars.Infrastructure.Contracts.EntitiesDB;
using StarWars.Infrastructure.Impl.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Impl
{
    public class PlanetsDBRepository : IPlanetsDBRepository
    {
        private readonly SWDBContext _swdbContext;

        public PlanetsDBRepository(SWDBContext swdbContext)
        {
            _swdbContext = swdbContext;
        }

      

        public void InsertOrUpdate(List<Planet> dbEntityList)
        {
            foreach (Planet candidateRow in dbEntityList)
            {
                if (_swdbContext.Planets.Any(existingRow => existingRow.NombrePlaneta == candidateRow.NombrePlaneta))
                {
                    Planet rowToUpdate = _swdbContext.Planets.First(existingRow => existingRow.NombrePlaneta == candidateRow.NombrePlaneta);
                    rowToUpdate.RotacionOrbitalEnDiasAlderaanos = candidateRow.RotacionOrbitalEnDiasAlderaanos;
                    rowToUpdate.PeriodoOrbitalEnHorasAlderaanas = candidateRow.PeriodoOrbitalEnHorasAlderaanas;
                    rowToUpdate.Clima = candidateRow.Clima;
                    rowToUpdate.Poblacion = candidateRow.Poblacion;
                    rowToUpdate.Url = candidateRow.Url;
                }
                else
                {
                    _swdbContext.Planets.Add(candidateRow);
                }
            }
            _swdbContext.SaveChanges();
        }

        public Planet? TryGet(string planetName)
        {
            return _swdbContext.Planets.FirstOrDefault(x => x.NombrePlaneta == planetName);
        }

        public void Insert(Planet dbEntity)
        {
            _swdbContext.Planets.Add(dbEntity);  // Agregar el objeto al DbContext
            _swdbContext.SaveChanges();
        }


    }
}
