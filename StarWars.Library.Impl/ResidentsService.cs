using StarWars.Infrastructure.Contracts;
using StarWars.Infrastructure.Contracts.EntitiesApi;
using StarWars.Infrastructure.Contracts.EntitiesDB;
using StarWars.Library.Contracts;
using StarWars.Library.Contracts.DTOs;
using StarWars.XCutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Library.Impl
{
    public class ResidentsService : IResidentsService
    {
      
        private readonly IPlanetsDBRepository _dbPlanetsRepository;
        private readonly IPlanetsApiRepository _apiPlanetsRepository;
        private readonly IPeopleApiRepository _apiPeopleRepository;

        public ResidentsService(
            IPlanetsDBRepository dbPlanetsRepository,
            IPlanetsApiRepository apiPlanetsRepository,
            IPeopleApiRepository apiPeopleRepository)
        {
            _dbPlanetsRepository = dbPlanetsRepository;
            _apiPlanetsRepository = apiPlanetsRepository;
            _apiPeopleRepository = apiPeopleRepository;
        }

        public async Task<GetResidentsByPlanetNameRsDto> GetResidentsByPlanetName(string planetName)
        {
            GetResidentsByPlanetNameRsDto result = new();

            try
            {
                Planet? candidatePlanet = _dbPlanetsRepository.TryGet(planetName);

                if (candidatePlanet != null)
                {
                    PlanetResidentListSWApiEntity? residentUrlList = await _apiPlanetsRepository.TryGetPlanetResidentListByPlanetUrl(candidatePlanet.Url);
                    if (residentUrlList != null && residentUrlList.ResidentUrlList != null)
                    {
                        foreach (string residentUrl in residentUrlList.ResidentUrlList)
                        {
                            PeopleSWApiEntity? candidateResident = await _apiPeopleRepository.TryGetByUrl(residentUrl);
                            if (candidateResident != null)
                            {
                                result.data ??= new List<string>();
                                result.data.Add(candidateResident.Name);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                result.errors ??= new List<GetResidentsByPlanetNameErrorEnum>();
                result.errors.Add(GetResidentsByPlanetNameErrorEnum.ServiceError);
            }

            return result;
        }
    }
}
