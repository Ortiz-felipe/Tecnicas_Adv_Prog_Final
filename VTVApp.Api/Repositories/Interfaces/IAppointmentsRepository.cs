using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Repositories.Interfaces
{
    public interface IAppointmentsRepository
    {
        Task<AppointmentDetailsDto?> GetAppointmentByIdAsync(Guid appointmentId, CancellationToken cancellationToken);
        Task<IEnumerable<AppointmentListDto>> GetAppointmentsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<AppointmentOperationResultDto> CreateAppointmentAsync(CreateAppointmentDto appointmentCreateDto, CancellationToken cancellationToken);
        Task<AppointmentOperationResultDto> UpdateAppointmentAsync(UpdateAppointmentDto appointmentUpdateDto, CancellationToken cancellationToken);
        Task<AppointmentOperationResultDto> DeleteAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken);
        Task<AvailableAppointmentSlotsDto> GetAvailableAppointmentSlotsAsync(DateTime date, CancellationToken cancellationToken);
        Task<IEnumerable<AppointmentListDto>> GetAppointmentsByRecheckRequiredAsync(CancellationToken cancellationToken);
        Task<bool> IsAppointmentAvailableAsync(Guid appointmentId, CancellationToken cancellationToken);
    }
}