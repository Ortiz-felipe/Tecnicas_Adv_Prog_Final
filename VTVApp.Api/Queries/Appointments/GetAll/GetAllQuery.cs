using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Appointments.GetAll
{
    public class GetAllQuery : IRequest<IActionResult>
    {
    }
}
