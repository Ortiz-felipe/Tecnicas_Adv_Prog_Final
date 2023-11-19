using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Appointments;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Appointments.GetLatestAppointmentByUserID
{
    public class Handler : IRequestHandler<GetLatestAppointmentByUserIdQuery, IActionResult>
    {
        private readonly IAppointmentsRepository _appointmentRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IAppointmentsRepository appointmentRepository, ILogger<Handler> logger)
        {
            _appointmentRepository =
                appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetLatestAppointmentByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentRepository.GetLatestAppointmentByUserIdAsync(request.UserId, cancellationToken);
                return appointment == null ? this.NoContent() : this.Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, AppointmentErrors.GetLatestAppointmentError(request.UserId).Message);
                return this.InternalServerError(AppointmentErrors.GetLatestAppointmentError(request.UserId));
            }
        }
    }
}
