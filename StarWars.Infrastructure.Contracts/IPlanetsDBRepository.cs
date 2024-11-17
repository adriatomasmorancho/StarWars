using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StarWars.Infrastructure.Contracts.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Contracts
{
    public interface IPlanetsDBRepository
    {
        void InsertOrUpdate(List<Planet> dbEntityList);

        Planet? TryGet(string planetName);

       void Insert(Planet dbEntity);
    }
}
