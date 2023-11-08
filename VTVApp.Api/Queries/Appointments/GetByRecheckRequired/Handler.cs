using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Appointments.AppointmentErrors;

namespace VTVApp.Api.Queries.Appointments.GetByRecheckRequired
{
    public class Handler : IRequestHandler<GetByRecheckRequiredQuery, IActionResult>
    {
        private readonly IAppointmentsRepository _appointmentRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IAppointmentsRepository appointmentRepository, ILogger<Handler> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(GetByRecheckRequiredQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var recheckRequiredAppointments = await _appointmentRepository.GetAppointmentsByRecheckRequiredAsync(cancellationToken);
                return this.Ok(recheckRequiredAppointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetRecheckRequiredAppointmentsError.Message);
                return this.InternalServerError(GetRecheckRequiredAppointmentsError);
            }
        }
    }
}
