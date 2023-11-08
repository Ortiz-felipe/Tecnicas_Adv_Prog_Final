using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Appointments.AppointmentErrors;

namespace VTVApp.Api.Queries.Appointments.GetAvailableSlots
{
    public class Handler : IRequestHandler<GetAvailableSlotsQuery, IActionResult>
    {
        private readonly IAppointmentsRepository _appointmentRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IAppointmentsRepository appointmentRepository, ILogger<Handler> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(GetAvailableSlotsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var availableSlots = await _appointmentRepository.GetAvailableAppointmentSlotsAsync(Convert.ToDateTime(request.Date), cancellationToken);
                return this.Ok(availableSlots);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetAvailableSlotsError.Message);
                return this.InternalServerError(GetAvailableSlotsError);
            }
        }
    }
}
