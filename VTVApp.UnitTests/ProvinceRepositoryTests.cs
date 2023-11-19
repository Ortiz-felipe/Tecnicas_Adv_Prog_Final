using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Provinces;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories;

namespace VTVApp.UnitTests
{
    public class ProvinceRepositoryTests
    {
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly VTVDataContext _context;
        private readonly ProvinceRepository _repository;
        private readonly Guid _userId = Guid.NewGuid();
        private readonly int _provinceId = 1;
        private readonly Guid _cityId = Guid.NewGuid();
        private readonly Guid _appointmentId = Guid.NewGuid();
        private readonly Guid _inspectionId = Guid.NewGuid();

        public ProvinceRepositoryTests()
        {
            _context = CreateInMemoryDataContext();
            _repository = new ProvinceRepository(_context, _mapper);
            SeedProvincesData();

            _mapper.Map<ProvinceDto>(Arg.Is<Province>(x => x != null))
                .Returns(callInfo =>
                {
                    var province = callInfo.Arg<Province>();
                    return new ProvinceDto()
                    {
                        Id = province.Id,
                        Name = province.Name
                    };
                });

            _mapper.Map<IEnumerable<ProvinceDto>>(Arg.Any<IEnumerable<Province>>())
                .Returns(callInfo =>
                {
                    var provinces = callInfo.Arg<IEnumerable<Province>>();
                    return provinces.Select(province => new ProvinceDto()
                    {
                        Id = province.Id,
                        Name = province.Name
                    });
                });
        }

        private void SeedProvincesData()
        {
            var provinces = new List<Province>
            {
                new Province
                {
                    Id = _provinceId,
                    Name = "Province 1"
                },
                new Province
                {
                    Id = 2,
                    Name = "Province 2"
                },
            };

            _context.Provinces.AddRange(provinces);
            _context.SaveChanges();
            
        }

        [Fact]
        public async Task GetAllProvincesAsync_ShouldReturnAllProvinces()
        {
            // Arrange
            var expectedProvinces = new List<ProvinceDto>
            {
                new ProvinceDto
                {
                    Id = _provinceId,
                    Name = "Province 1"
                },
                new ProvinceDto
                {
                    Id = 2,
                    Name = "Province 2"
                },
            };

            // Act
            var provinces = await _repository.GetAllProvincesAsync(default);

            // Assert
            provinces.Should().BeEquivalentTo(expectedProvinces);
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
    }
}
