using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Appointments;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Commands.Appointments.CreateAppointment
{
    public class Handler : IRequestHandler<CreateAppointmentCommand, IActionResult>
    {
        private readonly IAppointmentsRepository _appointmentRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IAppointmentsRepository appointmentRepository, ILogger<Handler> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentRepository.CreateAppointmentAsync(request.Body, cancellationToken);
                return this.CreatedAtRoute("GetAppointmentByIdAsync", new { appointmentId = appointment.AppointmentId }, appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, AppointmentErrors.CreateAppointmentError.Message);
                return this.InternalServerError(AppointmentErrors.CreateAppointmentError);
            }
        }
    }
}
