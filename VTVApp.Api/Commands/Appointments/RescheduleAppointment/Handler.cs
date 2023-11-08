using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Appointments;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Commands.Appointments.RescheduleAppointment
{
    public class Handler : IRequestHandler<RescheduleAppointmentCommand, IActionResult>
    {
        private readonly IAppointmentsRepository _appointmentRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IAppointmentsRepository appointmentRepository, ILogger<Handler> logger)
        {
            _appointmentRepository =
                appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentRepository.UpdateAppointmentAsync(request.Body, cancellationToken);

                if (!appointment.Success)
                {
                    return this.NotFound(AppointmentErrors.GetAppointmentNotFoundError(request.AppointmentId));
                }

                return this.Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, AppointmentErrors.UpdateAppointmentError.Message);
                return this.InternalServerError(AppointmentErrors.UpdateAppointmentError);
            }
        }
    }
}
