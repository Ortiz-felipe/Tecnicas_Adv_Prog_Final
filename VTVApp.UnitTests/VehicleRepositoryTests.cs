using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Vehicle;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories;

namespace VTVApp.UnitTests
{
    public class VehicleRepositoryTests
    {
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly VTVDataContext _context;
        private readonly VehicleRepository _repository;
        private readonly Guid _userId = Guid.NewGuid();
        private readonly int _provinceId = 1;
        private readonly Guid _vehicleId = Guid.NewGuid();
        private readonly Guid _cityId = Guid.NewGuid();
        private readonly Guid _appointmentId = Guid.NewGuid();


        public VehicleRepositoryTests()
        {
            _context = CreateInMemoryDataContext();
            _repository = new VehicleRepository(_context, _mapper);
            SeedVehiclesData();

            _mapper.Map<VehicleDto>(Arg.Is<Vehicle>(v => v == null))
                .Returns((VehicleDto)null);

            _mapper.Map<VehicleDto>(Arg.Is<Vehicle>(v => v != null))
                .Returns(call => MapVehicleToVehicleDto(call.Arg<Vehicle>()));

            _mapper.Map<IEnumerable<VehicleDto>>(Arg.Any<IEnumerable<Vehicle>>())
                .Returns(call => MapVehiclesToVehicleDtos(call.Arg<IEnumerable<Vehicle>>()));

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

        private void SeedVehiclesData()
        {
            if (!_context.Provinces.Any())
            {
                var province = new Province
                {
                    Id = _provinceId, // Assuming ProvinceId is a property with a unique value
                    Name = "Test Province"
                };
                _context.Provinces.Add(province);
            }

            if (!_context.Cities.Any())
            {
                var city = new City
                {
                    Id = _cityId, // Assuming CityId is a property with a unique value
                    Name = "Test City",
                    ProvinceId = _provinceId,
                    PostalCode = "123456"
                };
                _context.Cities.Add(city);
            }

            if (!_context.Users.Any())
            {
                var user = new User
                {
                    Id = _userId, // Assuming UserId is a property with a unique value
                    FullName = "John Doe",
                    Email = "johndoe@gmail.com",
                    PhoneNumber = "1144778855",
                    PasswordHash = "123456",
                    CityId = _cityId,
                    ProvinceId = _provinceId
                };
                _context.Users.Add(user);
            }

            if (!_context.Vehicles.Any())
            {
                var vehicle = new Vehicle
                {
                    Id = _vehicleId, // Assuming VehicleId is a property with a unique value
                    LicensePlate = "ABC123",
                    Brand = "Test Brand",
                    Model = "Test Model",
                    Color = "Test Color",
                    Year = 2020,
                    UserId = _userId,
                    IsFavorite = true
                };
                _context.Vehicles.Add(vehicle);
            }

            if (!_context.Appointments.Any())
            {
                var appointment = new Appointment
                {
                    Id = _appointmentId, // Assuming AppointmentId is a property with a unique value
                    Date = DateTime.UtcNow,
                    Time = TimeSpan.FromHours(10),
                    Status = AppointmentStatus.Pending,
                    UserId = _userId,
                    VehicleId = _vehicleId
                };
                _context.Appointments.Add(appointment);
            }

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetVehicleByIdAsync_ReturnsVehicle()
        {
            // Act
            var result = await _repository.GetVehicleByIdAsync(_vehicleId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(_vehicleId);
        }

        [Fact]
        public async Task GetVehicleByIdAsync_ReturnsNullWhenVehicleDoesNotExist()
        {
            // Act
            var result = await _repository.GetVehicleByIdAsync(Guid.NewGuid(), CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetVehiclesByUserIdAsync_ReturnsVehicles()
        {
            // Act
            var result = await _repository.GetVehiclesByUserIdAsync(_userId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetVehiclesByUserIdAsync_ReturnsEmptyListWhenUserDoesNotHaveVehicles()
        {
            // Act
            var result = await _repository.GetVehiclesByUserIdAsync(Guid.NewGuid(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetFavoriteVehiclesByUserIdAsync_ReturnsFavoriteVehicles()
        {
            // Act
            var result = await _repository.GetFavoriteVehicleByUserIdAsync(_userId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetFavoriteVehiclesByUserIdAsync_ReturnsEmptyListWhenUserDoesNotHaveFavoriteVehicles()
        {
            // Act
            var result = await _repository.GetFavoriteVehicleByUserIdAsync(Guid.NewGuid(), CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        private VehicleDto MapVehicleToVehicleDto(Vehicle vehicle)
        {
            // This is a simple example. You may need to adjust the mapping logic based on your actual requirements.
            return new VehicleDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Color = vehicle.Color,
                Year = vehicle.Year,
                isFavorite = vehicle.IsFavorite,
            };
        }

        private IEnumerable<VehicleDto> MapVehiclesToVehicleDtos(IEnumerable<Vehicle> vehicles)
        {
            // Assuming you have a method to map a single Vehicle to VehicleDto
            return vehicles.Select(v => MapVehicleToVehicleDto(v)).ToList();
        }
    }
}
