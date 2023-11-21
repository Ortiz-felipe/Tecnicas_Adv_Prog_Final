using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Repositories
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly IVtvDataContext _dataContext;
        private readonly IMapper _mapper;

        public AppointmentsRepository(IVtvDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentListDto>> GetAppointmentsAsync(CancellationToken cancellationToken)
        {
            var appointments = await _dataContext.Appointments
                .Include(a => a.Vehicle)
                .Include(a => a.User)
                .Where(a => a.Date.Date == DateTime.UtcNow.Date && a.Status == AppointmentStatus.Pending)
                .Select(a => new AppointmentListDto
                {
                    Id = a.Id,
                    AppointmentDate = a.Date.Add(a.Time),
                    VehicleLicensePlate = a.Vehicle.LicensePlate,
                    UserFullName = a.User.FullName,
                    AppointmentStatus = (int)a.Status
                })
                .ToListAsync(cancellationToken);

            return appointments;
        }

        public async Task<AppointmentDetailsDto?> GetAppointmentByIdAsync(Guid appointmentId, CancellationToken cancellationToken)
        {
            var appointment = await _dataContext.Appointments
                .Include(a => a.Vehicle)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == appointmentId, cancellationToken);

            return _mapper.Map<AppointmentDetailsDto?>(appointment);
        }

        public async Task<IEnumerable<AppointmentListDto>> GetAppointmentsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var appointments = await _dataContext.Appointments
                .Include(a => a.Vehicle)
                .Include(a => a.User)
                .Where(a => a.UserId == userId && a.Date <= DateTime.Now)
                .Select(a => new AppointmentListDto
                {
                    Id = a.Id,
                    AppointmentDate = a.Date.Add(a.Time),
                    VehicleLicensePlate = a.Vehicle.LicensePlate,
                    UserFullName = a.User.FullName,
                    AppointmentStatus = (int) a.Status
                })
                .ToListAsync(cancellationToken);

            return appointments;
        }

        public async Task<AppointmentDetailsDto?> GetLatestAppointmentByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var appointment = await _dataContext.Appointments
                .Include(a => a.Vehicle)
                .Include(a => a.User)
                .Include(a => a.Inspection)
                .Where(a => a.UserId == userId) // Assuming dates are stored in UTC
                .OrderByDescending(a => a.Date)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<AppointmentDetailsDto?>(appointment);
        }

        public async Task<AppointmentOperationResultDto> CreateAppointmentAsync(CreateAppointmentDto appointmentCreateDto, CancellationToken cancellationToken)
        {
            var appointment = _mapper.Map<Appointment>(appointmentCreateDto);

            await _dataContext.Appointments.AddAsync(appointment, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AppointmentOperationResultDto>(appointment);
        }

        public async Task<AppointmentOperationResultDto> UpdateAppointmentAsync(UpdateAppointmentDto appointmentUpdateDto, CancellationToken cancellationToken)
        {
            var appointmentToUpdate = await _dataContext.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentUpdateDto.Id, cancellationToken);

            if (appointmentToUpdate == null)
            {
                // If the appointment doesn't exist, we return an unsuccessful operation result
                // No exception is thrown here as it's a valid scenario to not find a resource
                return new AppointmentOperationResultDto
                {
                    Success = false,
                    Message = $"No appointment found with ID {appointmentUpdateDto.Id}"
                };
            }

            // Update the appointment date
            appointmentToUpdate.Date = appointmentUpdateDto.NewAppointmentDate;

            // Update the appointment in the database context
            _dataContext.Appointments.Update(appointmentToUpdate);
            await _dataContext.SaveChangesAsync(cancellationToken);

            // Map the updated appointment entity to the AppointmentOperationResultDto
            var resultDto = _mapper.Map<AppointmentOperationResultDto>(appointmentToUpdate);
            resultDto.Success = true;
            resultDto.Message = "Appointment updated successfully.";

            return resultDto;
        }

        public async Task<AppointmentOperationResultDto> DeleteAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<AvailableAppointmentSlotsDto> GetAvailableAppointmentSlotsAsync(DateTime date, CancellationToken cancellationToken)
        {
            // Retrieve all appointments for the specific day.
            var appointmentsOnDate = await _dataContext.Appointments
                .Where(a => a.Date.Date == date.Date)
                .ToListAsync(cancellationToken);

            var businessStart = new TimeSpan(8, 0, 0); // Business starts at 8 AM
            var businessEnd = new TimeSpan(17, 0, 0); // Business ends at 5 PM
            var appointmentDuration = TimeSpan.FromMinutes(30); // Each slot is 30 minutes long

            var availableSlots = new List<DateTime>();

            for (var slot = businessStart; slot < businessEnd; slot = slot.Add(appointmentDuration))
            {
                var appointmentTime = date.Date + slot;

                // Check if the slot is already taken
                if (appointmentsOnDate.All(a => a.Date.TimeOfDay != slot))
                {
                    availableSlots.Add(appointmentTime);
                }
            }

            var resultDto = new AvailableAppointmentSlotsDto
            {
                AvailableSlots = availableSlots
            };

            return resultDto;
        }

        public async Task<IEnumerable<AppointmentListDto>> GetAppointmentsByRecheckRequiredAsync(CancellationToken cancellationToken)
        {
            var appointments = await _dataContext.Appointments
                .Include(a => a.Inspection)
                .ThenInclude(i => i.Checkpoints)
                .Include(a => a.User) 
                .Include(a => a.Vehicle) 
                .Where(a => a.Inspection.TotalScore == 40 ||
                            a.Inspection.Checkpoints.Any(c => c.Score < 5))
                .Select(a => new AppointmentListDto
                {
                    Id = a.Id,
                    AppointmentDate = a.Date.Add(a.Time),
                    UserFullName = a.User.FullName, 
                    VehicleLicensePlate = a.Vehicle.LicensePlate 
                    
                })
                .ToListAsync(cancellationToken);

            return appointments;
        }

        public async Task<bool> IsAppointmentAvailableAsync(Guid appointmentId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
