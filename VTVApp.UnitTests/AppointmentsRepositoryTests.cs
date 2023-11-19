using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.DTOs.Users;
using VTVApp.Api.Models.DTOs.Vehicle;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using User = VTVApp.Api.Models.Entities.User;

namespace VTVApp.UnitTests;

public class AppointmentsRepositoryTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private VTVDataContext _context;
    private AppointmentsRepository _repository;
    private Guid _userId = Guid.NewGuid();
    private int _provinceId = 1;
    private Guid _vehicleId = Guid.NewGuid();
    private Guid _cityId = Guid.NewGuid();
    private Guid _appointmentId = Guid.NewGuid();


    public AppointmentsRepositoryTest()
    {
        _context = CreateInMemoryDataContext();
        _repository = new AppointmentsRepository(_context, _mapper);
        SeedAppointmentsData();

        _mapper.Map<AppointmentDetailsDto>(Arg.Is<Appointment>(x => x != null))
            .Returns(callInfo =>
            {
                var appointment = callInfo.Arg<Appointment>();
                return new AppointmentDetailsDto
                {
                    Id = appointment.Id,
                    AppointmentDate = appointment.Date.Add(appointment.Time)
                };
            });

        // Mock AutoMapper configuration for AppointmentListDto
        _mapper.Map<AppointmentListDto>(Arg.Is<Appointment>(x => x != null))
            .Returns(callInfo =>
            {
                var appointment = callInfo.Arg<Appointment>();
                return new AppointmentListDto
                {
                    Id = appointment.Id,
                    AppointmentDate = appointment.Date.Add(appointment.Time),
                    UserFullName = appointment.User != null ? appointment.User.FullName : string.Empty,
                    VehicleLicensePlate = appointment.Vehicle != null ? appointment.Vehicle.LicensePlate : string.Empty,
                    AppointmentStatus = (int)appointment.Status
                };
            });
    }

    private void SeedAppointmentsData()
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
                UserId = _userId
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
    public async Task GetAppointmentsAsync_ReturnsAppointmentsForCurrentDayAndPendingStatus()
    {
        // Act
        var result = await _repository.GetAppointmentsAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAppointmentByIdAsync_ReturnsCorrectAppointmentDetails()
    {
        // Act
        var result = await _repository.GetAppointmentByIdAsync(_appointmentId, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(_appointmentId);
    }

    [Fact]
    public async Task GetAppointmentsByUserIdAsync_ReturnsAppointmentsForUser()
    {
        // Act
        var result = await _repository.GetAppointmentsByUserIdAsync(_userId, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
    }
}