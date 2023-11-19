using AutoMapper;
using NSubstitute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Cities;
using VTVApp.Api.Models.DTOs.Provinces;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories;

namespace VTVApp.UnitTests
{
    public class CityRepositoryTests
    {
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly VTVDataContext _context;
        private readonly CityRepository _repository;
        private readonly Guid _userId = Guid.NewGuid();
        private readonly int _provinceId = 1;
        private readonly Guid _vehicleId = Guid.NewGuid();
        private readonly Guid _cityId = Guid.NewGuid();
        private readonly Guid _appointmentId = Guid.NewGuid();
        private readonly Guid _inspectionId = Guid.NewGuid();

        public CityRepositoryTests()
        {
            _context = CreateInMemoryDataContext();
            _repository = new CityRepository(_context, _mapper);
            SeedCitiesData();

            _mapper.Map<CityDetailsDto>(Arg.Is<City>(x => x != null))
                .Returns(callInfo =>
                {
                    var city = callInfo.Arg<City>();
                    return new CityDetailsDto
                    {
                        Id = city.Id,
                        Name = city.Name,
                        Province = new ProvinceDto
                        {
                            Id = city.Province.Id,
                            Name = city.Province.Name
                        }
                    };
                });

            _mapper.Map<IEnumerable<CityDetailsDto>>(Arg.Any<IEnumerable<City>>())
                .Returns(call => MapCitiesToCityDetailsDto(call.Arg<IEnumerable<City>>()));
        }

        private IEnumerable<CityDetailsDto> MapCitiesToCityDetailsDto(IEnumerable<City> cities)
        {
            return cities.Select(c => new CityDetailsDto
            {
                Id = c.Id,
                Name = c.Name,
                Province = new ProvinceDto
                {
                    Id = c.Province.Id,
                    Name = c.Province.Name
                }
            }).ToList();
        }

        private void SeedCitiesData()
        {
            var province = new Province
            {
                Id = _provinceId,
                Name = "Province 1"
            };

            var city = new City
            {
                Id = _cityId,
                Name = "City 1",
                ProvinceId = _provinceId,
                PostalCode = "12345",
            };

            _context.Provinces.Add(province);
            _context.Cities.Add(city);
            _context.SaveChanges(); 
            
        }

        private VTVDataContext CreateInMemoryDataContext()
        {
            var options = new DbContextOptionsBuilder<VTVDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new VTVDataContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task GetAllCitiesAsync_ShouldReturnAllCities()
        {
            // Arrange
            var expectedCities = await _context.Cities
                .Include(c => c.Province)
                .ToListAsync();

            // Act
            var actualCities = await _repository.GetAllCitiesAsync(CancellationToken.None);

            // Assert
            Assert.Equal(expectedCities.Count, actualCities.Count());
            Assert.Equal(expectedCities[0].Id, actualCities.ElementAt(0).Id);
            Assert.Equal(expectedCities[0].Name, actualCities.ElementAt(0).Name);
            Assert.Equal(expectedCities[0].Province.Id, actualCities.ElementAt(0).Province.Id);
            Assert.Equal(expectedCities[0].Province.Name, actualCities.ElementAt(0).Province.Name);
        }

        [Fact]
        public async Task GetCityByIdAsync_ShouldReturnCity()
        {
            // Arrange
            var expectedCity = await _context.Cities
                .Include(c => c.Province)
                .FirstOrDefaultAsync(c => c.Id == _cityId);

            // Act
            var actualCity = await _repository.GetCityByIdAsync(_cityId, CancellationToken.None);

            // Assert
            Assert.Equal(expectedCity.Id, actualCity.Id);
            Assert.Equal(expectedCity.Name, actualCity.Name);
            Assert.Equal(expectedCity.Province.Id, actualCity.Province.Id);
            Assert.Equal(expectedCity.Province.Name, actualCity.Province.Name);
        }
    }
}
