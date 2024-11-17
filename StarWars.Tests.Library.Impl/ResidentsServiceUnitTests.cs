using Moq;
using StarWars.Infrastructure.Contracts;
using StarWars.Infrastructure.Contracts.EntitiesApi;
using StarWars.Infrastructure.Contracts.EntitiesDB;
using StarWars.Library.Contracts.DTOs;
using StarWars.Library.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Tests.Library.Impl
{
    public class ResidentsServiceUnitTests
    {
        [Fact]
        public async void GetResidentsByPlanetName_WhenNoErrors_ReturnNoErrors()
        {
            // Arrange
            Mock<IPlanetsDBRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.TryGet(It.IsAny<string>()))
                .Returns(new Planet());

            Mock<IPlanetsApiRepository> mockedSWDBPlanetsRepository = new();
            mockedSWDBPlanetsRepository
                .Setup(x => x.TryGetPlanetResidentListByPlanetUrl(It.IsAny<string>()))
                .ReturnsAsync(new PlanetResidentListSWApiEntity());

            Mock<IPeopleApiRepository> mockedSWApiPeopleRepository = new();
            mockedSWApiPeopleRepository
                .Setup(x => x.TryGetByUrl(It.IsAny<string>()))
                .ReturnsAsync(new PeopleSWApiEntity());

            ResidentsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object,
                mockedSWApiPeopleRepository.Object);

            const string VALIDPLANET = "Tatooine";

            // Act
            GetResidentsByPlanetNameRsDto result = await sut.GetResidentsByPlanetName(VALIDPLANET);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.errors);
            Assert.NotNull(result.data);
        }
    }
}
