using FluentValidation;
using VTVApp.Api.Models.DTOs.Appointments;

namespace VTVApp.Api.Commands.Appointments.CreateAppointment
{
    public class ValidatorCollection : AbstractValidator<CreateAppointmentCommand>
    {
        public ValidatorCollection()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Body)
                .NotNull().WithMessage("Request body must not be null")
                .SetValidator(new CreateAppointmentDtoValidator());
        }
    }

    public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.AppointmentDate)
                .NotEmpty().WithMessage("Appointment date is required.")
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Appointment date cannot be in the past.");

            RuleFor(dto => dto.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(dto => dto.VehicleId)
                .NotEmpty().WithMessage("Vehicle ID is required.");
        }
    }
}
