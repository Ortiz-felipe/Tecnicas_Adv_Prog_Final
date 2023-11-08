using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Appointments.GetAvailableSlots
{
    public class GetAvailableSlotsQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public string Date { get; set; }
    }
}
