using Moq;
using StarWars.Infrastructure.Contracts;
using StarWars.Infrastructure.Contracts.EntitiesApi;
using StarWars.Infrastructure.Contracts.EntitiesDB;
using StarWars.Library.Contracts.DTOs;
using StarWars.Library.Impl;
using StarWars.XCutting.Enums;

namespace StarWars.Tests.Library.Impl
{
    public class PlanetsServiceUnitTests
    {
        [Fact]
        public async void RefreshPlanets_WhenNoErrors_ReturnNoErrors()
        {
            // Arrange
            Mock<IPlanetsApiRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<PlanetSWApiEntity>());

            Mock<IPlanetsDBRepository> mockedSWDBPlanetsRepository = new();

            PlanetsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object);

            // Act
            RefreshAndListPlanetNamesRsDto result = await sut.RefreshPlanets();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.errors);
            Assert.NotNull(result.data);
        }

        [Fact]
        public async void RefreshPlanets_WhenSWApiError_ReturnSWApiError()
        {
            // Arrange
            Mock<IPlanetsApiRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.GetAll())
                .ThrowsAsync(new Exception());

            Mock<IPlanetsDBRepository> mockedSWDBPlanetsRepository = new();

            PlanetsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object);

            // Act
            RefreshAndListPlanetNamesRsDto result = await sut.RefreshPlanets();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(RefreshAndListPlanetNamesErrorEnum.SWApiErrorConnection, result.errors[0]);
        }

        [Fact]
        public async void RefreshPlanets_WhenMappingError_ReturnMappingError()
        {
            // Arrange
            Mock<IPlanetsApiRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<PlanetSWApiEntity>
                {
                    new()
                    {
                        Name = "",
                        RotationPeriod = "0",
                        OrbitalPeriod = "0",
                        Climate = "",
                        Population = "",
                        Url = ""
                    }
                });

            Mock<IPlanetsDBRepository> mockedSWDBPlanetsRepository = new();

            PlanetsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object);

            // Act
            RefreshAndListPlanetNamesRsDto result = await sut.RefreshPlanets();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(RefreshAndListPlanetNamesErrorEnum.EntityMappingConnection, result.errors[0]);
        }

        [Fact]
        public async void RefreshPlanets_WhenSWDBError_ReturnSWDBError()
        {
            // Arrange
            Mock<IPlanetsApiRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<PlanetSWApiEntity>());

            Mock<IPlanetsDBRepository> mockedSWDBPlanetsRepository = new();
            mockedSWDBPlanetsRepository
                .Setup(x => x.InsertOrUpdate(It.IsAny<List<Planet>>()))
                .Throws(new Exception());

            PlanetsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object);

            // Act
            RefreshAndListPlanetNamesRsDto result = await sut.RefreshPlanets();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(RefreshAndListPlanetNamesErrorEnum.SWDbErrorConnection, result.errors[0]);
        }
    }
}