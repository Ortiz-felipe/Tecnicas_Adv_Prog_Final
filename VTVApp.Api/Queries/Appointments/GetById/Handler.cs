using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Appointments.AppointmentErrors;

namespace VTVApp.Api.Queries.Appointments.GetById
{
    public class Handler : IRequestHandler<GetByIdQuery, IActionResult>
    {
        private readonly IAppointmentsRepository _appointmentRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IAppointmentsRepository appointmentRepository, ILogger<Handler> logger)
        {
            _appointmentRepository =
                appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentId, cancellationToken);
                return appointment == null ? this.NotFound(GetAppointmentNotFoundError(request.AppointmentId)) : this.Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetAppointmentByIdError(request.AppointmentId).Message);
                return this.InternalServerError(GetAppointmentByIdError(request.AppointmentId));
            }
        }
    }
}
