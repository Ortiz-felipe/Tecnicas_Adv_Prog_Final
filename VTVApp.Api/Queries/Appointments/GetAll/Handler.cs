using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Appointments;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Appointments.GetAll
{
    public class Handler : IRequestHandler<GetAllQuery, IActionResult>
    {
        private readonly IAppointmentsRepository _appointmentRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IAppointmentsRepository appointmentRepository, ILogger<Handler> logger)
        {
            _appointmentRepository =
                appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointments = await _appointmentRepository.GetAppointmentsAsync(cancellationToken);
                return this.Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, AppointmentErrors.GetAllAppointmentsError.Message);
                return this.InternalServerError(AppointmentErrors.GetAllAppointmentsError);
            }
        }
    }
}
