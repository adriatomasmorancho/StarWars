using StarWars.Infrastructure.Contracts;
using StarWars.Infrastructure.Contracts.EntitiesApi;
using StarWars.Infrastructure.Contracts.EntitiesDB;
using StarWars.Library.Contracts;
using StarWars.Library.Contracts.DTOs;
using StarWars.XCutting.Enums;
using System;

namespace StarWars.Library.Impl
{
    public class PlanetsService : IPlanetsService
    {

        private readonly IPlanetsApiRepository _apiPlanetsRepository;
        private readonly IPlanetsDBRepository _dbPlanetsRepository;

        public PlanetsService(IPlanetsApiRepository apiPlanetsRepository,
            IPlanetsDBRepository dbPlanetsRepository)
        {
            _apiPlanetsRepository = apiPlanetsRepository;
            _dbPlanetsRepository = dbPlanetsRepository;
        }

        public async Task<RefreshAndListPlanetNamesRsDto> RefreshPlanets()
        {
            RefreshAndListPlanetNamesRsDto result = new();

            List<PlanetSWApiEntity> swapiEntityList;
            List<Planet> dbEntityList;
            try
            {
                swapiEntityList = await _apiPlanetsRepository.GetAll();
            }
            catch (Exception)
            {
                result.errors = new List<RefreshAndListPlanetNamesErrorEnum> { RefreshAndListPlanetNamesErrorEnum.SWApiErrorConnection };
                return result;
            }
            try
            {
                dbEntityList = swapiEntityList.Select(x => new Planet
                {

                    NombrePlaneta = x.Name,
                    RotacionOrbitalEnDiasAlderaanos = int.Parse(x.RotationPeriod),
                    PeriodoOrbitalEnHorasAlderaanas = int.Parse(x.OrbitalPeriod),
                    Clima = x.Climate,
                    Poblacion = x.Population == "unknown" ? "0" : long.Parse(x.Population).ToString(),
                    Url = x.Url
                }).ToList();
            }
            catch (Exception)
            {
                result.errors = new List<RefreshAndListPlanetNamesErrorEnum> { RefreshAndListPlanetNamesErrorEnum.EntityMappingConnection };
                return result;
            }
            try
            {
                _dbPlanetsRepository.InsertOrUpdate(dbEntityList);
            }
            catch (Exception)
            {
                result.errors = new List<RefreshAndListPlanetNamesErrorEnum> { RefreshAndListPlanetNamesErrorEnum.SWDbErrorConnection };
                return result;
            }

            result.data = swapiEntityList.Select(x => x.Name).ToList();
            return result;
        }

       

       public async Task<CreatePlanetRsDto> CreatePlanets(string name, int rotation, int period, string climate, string poblation, string url)
        {
            CreatePlanetRsDto result = new();

            Planet dbEntity = new();

            dbEntity.NombrePlaneta = name;

            dbEntity.Url = url;

            dbEntity.RotacionOrbitalEnDiasAlderaanos = rotation;

            dbEntity.PeriodoOrbitalEnHorasAlderaanas = period;

            dbEntity.Clima = climate;

            dbEntity.Poblacion = poblation;



            // Comprovación si todos los datos son correctos

            try
            {
                _dbPlanetsRepository.Insert(dbEntity);

                result.NombrePlaneta = dbEntity.NombrePlaneta;

                result.Url = dbEntity.Url;

                result.RotacionOrbitalEnDiasAlderaanos = dbEntity.RotacionOrbitalEnDiasAlderaanos;

                result.PeriodoOrbitalEnHorasAlderaanas = dbEntity.PeriodoOrbitalEnHorasAlderaanas;

                result.Clima = dbEntity.Clima;

                result.Poblacion = dbEntity.Poblacion;

            }
            catch (Exception)
            {

            }

            return result;
        }
    }
}
