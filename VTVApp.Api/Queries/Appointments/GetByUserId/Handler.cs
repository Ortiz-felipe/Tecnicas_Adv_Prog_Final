using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Appointments.AppointmentErrors;

namespace VTVApp.Api.Queries.Appointments.GetByUserId
{
    public class Handler : IRequestHandler<GetByUserIdQuery, IActionResult>
    {
        private readonly IAppointmentsRepository _appointmentRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IAppointmentsRepository appointmentRepository, ILogger<Handler> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointments = await _appointmentRepository.GetAppointmentsByUserIdAsync(request.UserId, cancellationToken);
                return appointments.Any()
                    ? this.Ok(appointments)
                    : this.NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetUserAppointmentsError(request.UserId).Message);
                return this.InternalServerError(GetUserAppointmentsError(request.UserId));
            }
        }
    }
}
