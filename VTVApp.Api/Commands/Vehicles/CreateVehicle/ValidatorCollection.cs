using FluentValidation;
using VTVApp.Api.Models.DTOs.Vehicle;

namespace VTVApp.Api.Commands.Vehicles.CreateVehicle
{
    public class ValidatorCollection : AbstractValidator<CreateVehicleCommand>
    {
        public ValidatorCollection()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Body)
                .NotNull().WithMessage("Request body must not be null")
                .SetValidator(new CreateVehicleDtoValidator());
        }
    }

    public class CreateVehicleDtoValidator : AbstractValidator<CreateVehicleDto>
    {
        public CreateVehicleDtoValidator()
        {
            RuleFor(v => v.LicensePlate)
                .NotEmpty().WithMessage("License plate is required.");

            RuleFor(v => v.Make)
                .NotEmpty().WithMessage("Make is required.")
                .MaximumLength(50).WithMessage("Make must not exceed 50 characters.");

            RuleFor(v => v.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(50).WithMessage("Model must not exceed 50 characters.");

            RuleFor(v => v.Color)
                .NotEmpty().WithMessage("Color is required.")
                .MaximumLength(30).WithMessage("Color must not exceed 30 characters.");

            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
