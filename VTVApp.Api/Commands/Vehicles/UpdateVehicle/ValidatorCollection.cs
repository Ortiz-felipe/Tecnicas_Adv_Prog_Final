using FluentValidation;
using VTVApp.Api.Models.DTOs.Vehicle;

namespace VTVApp.Api.Commands.Vehicles.UpdateVehicle
{
    public class ValidatorCollection : AbstractValidator<UpdateVehicleCommand>
    {
        public ValidatorCollection()
        {
            RuleFor(command => command.VehicleId)
                .NotEmpty().WithMessage("VehicleId from route must not be empty.");

            RuleFor(command => command.Body)
                .NotNull().WithMessage("Request body must not be null")
                .SetValidator(new VehicleUpdateDtoValidator())
                .DependentRules(() =>
                {
                    // Ensure that the route ID matches the body ID
                    RuleFor(command => command.Body)
                        .Must((command, dto) => dto.Id == command.VehicleId)
                        .WithMessage("VehicleId from route and body must match.");
                });
        }
    }

    public class VehicleUpdateDtoValidator : AbstractValidator<VehicleUpdateDto>
    {
        public VehicleUpdateDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Vehicle ID must not be empty.");
            
            RuleFor(dto => dto.LicensePlate)
                .NotEmpty().WithMessage("License plate is required.");

            RuleFor(dto => dto.Brand)
                .NotEmpty().WithMessage("Make is required.")
                .MaximumLength(50).WithMessage("Make must not exceed 50 characters.");

            RuleFor(dto => dto.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(50).WithMessage("Model must not exceed 50 characters.");

            RuleFor(dto => dto.Year)
                .InclusiveBetween(1900, DateTime.UtcNow.Year).WithMessage("Year must be between 1900 and the current year.");
        }
    }
}
