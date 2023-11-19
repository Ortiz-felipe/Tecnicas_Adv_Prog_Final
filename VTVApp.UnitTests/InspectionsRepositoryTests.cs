using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Checkpoints;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.DTOs.Vehicle;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories;

namespace VTVApp.UnitTests
{
    public class InspectionsRepositoryTests
    {
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly VTVDataContext _context;
        private readonly InspectionsRepository _repository;
        private readonly Guid _userId = Guid.NewGuid();
        private readonly int _provinceId = 1;
        private readonly Guid _vehicleId = Guid.NewGuid();
        private readonly Guid _cityId = Guid.NewGuid();
        private readonly Guid _appointmentId = Guid.NewGuid();
        private readonly Guid _inspectionId = Guid.NewGuid();

        public InspectionsRepositoryTests()
        {
            _context = CreateInMemoryDataContext();
            _repository = new InspectionsRepository(_context, _mapper);
            SeedInspectionsData();

            _mapper.Map<InspectionDetailsDto>(Arg.Is<Inspection>(x => x != null))
                .Returns(callInfo =>
                {
                    var inspection = callInfo.Arg<Inspection>();
                    return new InspectionDetailsDto
                    {
                        Id = inspection.Id,
                        InspectionDate = inspection.InspectionDate,
                        Status = (int)inspection.Status,
                        Checkpoints = inspection.Checkpoints.Select(c => new InspectionCheckpointDetailDto()
                        {
                            CheckpointId = c.Id,
                        }).ToList()
                    };
                });

            _mapper.Map<IEnumerable<InspectionListDto>>(Arg.Any<IEnumerable<Inspection>>())
                .Returns(call => MapInspectionsToInspectionListDto(call.Arg<IEnumerable<Inspection>>()));

            // Map from Inspection to InspectionOperationResultDto
            _mapper.Map<InspectionOperationResultDto>(Arg.Any<Inspection>())
                .Returns(call =>
                {
                    var inspection = call.Arg<Inspection>();
                    return new InspectionOperationResultDto
                    {
                        Success = true, // Assuming success for the sake of the example
                        Message = "Inspection added successfully",
                        InspectionDetails = new InspectionDetailsDto
                        {
                            Id = inspection.Id,
                            VehicleId = inspection.Appointment.VehicleId, // Assuming this is how VehicleId is obtained
                            InspectionDate = inspection.InspectionDate,
                            // Map other required properties
                            Checkpoints = inspection.Checkpoints.Select(c => new InspectionCheckpointDetailDto
                            {
                                CheckpointId = c.Id,
                                CheckpointName = c.Name,
                                Score = c.Score,
                                Comments = c.Comment,
                                // ... other mappings
                            }).ToList(),
                            OverallComments = inspection.Result, // Example mapping
                            Status = (int)inspection.Status
                        }
                    };
                });

            _mapper.Map<Inspection>(Arg.Any<CreateInspectionDto>())
                .Returns(call =>
                {
                    var dto = call.Arg<CreateInspectionDto>();
                    return new Inspection
                    {
                        // Map properties as required
                        InspectionDate = dto.InspectionDate,
                        AppointmentId = dto.AppointmentId,
                        Checkpoints = dto.Checkpoints.Select(c => new Checkpoint
                        {
                            Name = c.Name,
                            Score = c.Score,
                            Comment = c.Comment
                            // ... other mappings
                        }).ToList(),
                        TotalScore = 0,
                        Result = "",
                    };
                });
        }

        private void SeedInspectionsData()
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

            if (!_context.Inspections.Any())
            {
                var inspection = new Inspection()
                {
                    Id = _inspectionId,
                    AppointmentId = _appointmentId,
                    TotalScore = 0,
                    Result = "",
                    Checkpoints = new List<Checkpoint>()
                    {
                        new Checkpoint()
                        {
                            Id = Guid.NewGuid(),
                            InspectionId = Guid.NewGuid(),
                            Score = 0,
                            Comment = "",
                            Name = "Test"
                        }
                    }
                };

                _context.Inspections.Add(inspection);
            }

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetInspectionsByUserIdAsync_WhenCalled_ReturnsInspections()
        {
            // Arrange
            var expectedInspections = await _context.Inspections
                .Include(i => i.Appointment)
                .ThenInclude(a => a.Vehicle)
                .Include(i => i.Checkpoints)
                .Where(i => i.Appointment.UserId == _userId)
                .ToListAsync();

            // Act
            var inspections = await _repository.GetInspectionsByUserIdAsync(_userId, CancellationToken.None);

            // Assert
            Assert.NotNull(inspections);
            Assert.Equal(expectedInspections.Count, inspections.Count());
        }

        [Fact]
        public async Task GetInspectionByIdAsync_WhenCalled_ReturnsInspection()
        {
            // Arrange
            var expectedInspection = await _context.Inspections
                .Include(i => i.Appointment)
                .Include(i => i.Checkpoints)
                .FirstOrDefaultAsync(i => i.Id == _inspectionId);

            // Act
            var inspection = await _repository.GetInspectionByIdAsync(_inspectionId, CancellationToken.None);

            // Assert
            Assert.NotNull(inspection);
            Assert.Equal(expectedInspection.Id, inspection.Id);
        }

        [Fact]
        public async Task GetLatestInspectionByVehicleIdAsync_WhenCalled_ReturnsInspection()
        {
            // Arrange
            var expectedInspection = await _context.Inspections
                .Include(i => i.Appointment)
                .Include(i => i.Checkpoints)
                .Where(i => i.Appointment.VehicleId == _vehicleId)
                .OrderByDescending(i => i.Appointment.Date)
                .FirstOrDefaultAsync();

            // Act
            var inspection = await _repository.GetLatestInspectionByVehicleIdAsync(_vehicleId, CancellationToken.None);

            // Assert
            Assert.NotNull(inspection);
            Assert.Equal(expectedInspection.Id, inspection.Id);
        }

        [Fact]
        public async Task GetInspectionsByVehicleIdAsync_WhenCalled_ReturnsInspections()
        {
            // Arrange
            var expectedInspections = await _context.Inspections
                .Include(i => i.Appointment)
                .Include(i => i.Checkpoints)
                .Where(i => i.Appointment.VehicleId == _vehicleId)
                .ToListAsync();

            // Act
            var inspections = await _repository.GetInspectionsByVehicleIdAsync(_vehicleId, CancellationToken.None);

            // Assert
            Assert.NotNull(inspections);
            Assert.Equal(expectedInspections.Count, inspections.Count());
        }

        [Fact]
        public async Task GetInspectionsByRecheckRequiredAsync_WhenCalled_ReturnsInspections()
        {
            // Arrange
            var expectedInspections = await _context.Inspections
                .Include(i => i.Appointment)
                .Include(i => i.Checkpoints)
                .Where(i => i.Appointment.Status == AppointmentStatus.Pending)
                .ToListAsync();

            // Act
            var inspections = await _repository.GetInspectionsByRecheckRequiredAsync(CancellationToken.None);

            // Assert
            Assert.NotNull(inspections);
            Assert.Equal(expectedInspections.Count, inspections.Count());
        }

        [Fact]
        public async Task AddInspectionAsync_WhenCalled_ReturnsInspectionOperationResultDto()
        {
            // Arrange
            var inspection = new CreateInspectionDto()
            {
                VehicleId = _vehicleId,
                AppointmentId = _appointmentId,
                InspectionDate = DateTime.UtcNow,
                Checkpoints = new List<CheckpointListDto>()
                {
                    new CheckpointListDto()
                    {
                        Id = Guid.NewGuid(),
                        Score = 0,
                        Name = "Checkpoint Name",
                        Comment = "Checkpoint Comment"
                    }
                }
            };

            // Act
            var result = await _repository.AddInspectionAsync(inspection, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Inspection added successfully", result.Message);
            Assert.NotNull(result.InspectionDetails);
            Assert.Equal(inspection.VehicleId, result.InspectionDetails.VehicleId);
            Assert.Equal(inspection.InspectionDate, result.InspectionDetails.InspectionDate);
            Assert.Equal(inspection.Checkpoints.Count(), result.InspectionDetails.Checkpoints.Count);
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

        private IEnumerable<InspectionListDto> MapInspectionsToInspectionListDto(IEnumerable<Inspection> inspections)
        {
            return inspections.Select(MapInspectionToInspectionListDto).ToList();
        }

        private InspectionListDto MapInspectionToInspectionListDto(Inspection inspection)
        {
            return new InspectionListDto
            {
                Id = inspection.Id,
                InspectionDate = inspection.InspectionDate,
                VehicleId = inspection.Appointment.VehicleId,
                LicensePlate = inspection.Appointment.Vehicle.LicensePlate,
            };
        }
    }
}
