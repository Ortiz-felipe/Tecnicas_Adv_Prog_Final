using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Vehicle;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VTVDataContext _context;
        private readonly IMapper _mapper;

        public VehicleRepository(VTVDataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<VehicleDto?> GetVehicleByIdAsync(Guid vehicleId, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Appointments)
                .FirstOrDefaultAsync(v => v.Id == vehicleId, cancellationToken);

            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync(CancellationToken cancellationToken)
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Appointments)
                .Include(v => v.User)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<IEnumerable<VehicleDto>?> GetVehiclesByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Appointments)
                .Include(v => v.User)
                .Where(v => v.UserId == userId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<VehicleOperationResultDto> AddVehicleAsync(CreateVehicleDto createVehicleDto, CancellationToken cancellationToken)
        {
            var newVehicle = _mapper.Map<Vehicle>(createVehicleDto);

            await _context.Vehicles.AddAsync(newVehicle, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<VehicleOperationResultDto>(newVehicle);
        }

        public async Task<VehicleOperationResultDto> UpdateVehicleAsync(VehicleUpdateDto updateVehicleDto, CancellationToken cancellationToken)
        {
            var vehicleToUpdate = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == updateVehicleDto.Id, cancellationToken);

            if (vehicleToUpdate == null)
            {
                return new VehicleOperationResultDto
                {
                    Success = false,
                    Message = "Vehicle not found."
                };
            };

            vehicleToUpdate.LicensePlate = updateVehicleDto.LicensePlate;
            vehicleToUpdate.Name = updateVehicleDto.Make;
            vehicleToUpdate.Model = updateVehicleDto.Model;
            vehicleToUpdate.Year = updateVehicleDto.Year;

            _context.Vehicles.Update(vehicleToUpdate);
            await _context.SaveChangesAsync(cancellationToken);

            return new VehicleOperationResultDto
            {
                Id = vehicleToUpdate.Id,
                Success = true,
                Message = "Vehicle updated successfully."
            };
        }

        public async Task<OperationResultDto> DeleteVehicleAsync(Guid vehicleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehicleDto>> FindVehiclesAsync(VehicleFilterDto vehicleFilterDto, CancellationToken cancellationToken)
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Appointments)
                .Include(v => v.User)
                .Where(v =>
                                       (string.IsNullOrEmpty(vehicleFilterDto.LicensePlate) || v.LicensePlate.Contains(vehicleFilterDto.LicensePlate)) &&
                                                          (string.IsNullOrEmpty(vehicleFilterDto.Model) || v.Model.Contains(vehicleFilterDto.Model)) &&
                                                          (!vehicleFilterDto.Year.HasValue || v.Year == vehicleFilterDto.Year) &&
                                                          (!vehicleFilterDto.OwnerId.HasValue || v.UserId == vehicleFilterDto.OwnerId)
                                                      )
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }
    }
}
