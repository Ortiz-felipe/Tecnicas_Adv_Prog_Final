using FluentValidation;
using VTVApp.Api.Commands.Inspections.UpdateInspectionCheckpoint;
using VTVApp.Api.Models.DTOs.Checkpoints;
using VTVApp.Api.Models.DTOs.Inspections;

namespace VTVApp.Api.Commands.Inspections.UpdateInspection
{
    public class ValidatorCollection : AbstractValidator<UpdateInspectionCheckpointCommand>
    {
        public ValidatorCollection()
        {
            RuleFor(command => command.InspectionId)
                .NotEmpty().WithMessage("Inspection ID must not be empty.");

            RuleFor(command => command.CheckpointId)
                .NotEmpty().WithMessage("Checkpoint ID must not be empty.");

            RuleFor(command => command.Body)
                .NotNull().WithMessage("Update information must not be null.")
                .SetValidator(new UpdateInspectionDtoValidator());
        }
    }

    public class UpdateInspectionDtoValidator : AbstractValidator<UpdateInspectionDto>
    {
        public UpdateInspectionDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Inspection ID must not be empty.");

            RuleFor(dto => dto.AppointmentId)
                .NotEmpty().WithMessage("Appointment ID must not be empty.");

            RuleFor(dto => dto.Result)
                .NotEmpty().WithMessage("Result must not be empty.");

            RuleFor(dto => dto.TotalScore)
                .NotEmpty().WithMessage("Total score must not be empty.")
                .InclusiveBetween(0, 100).WithMessage("Total score must be between 0 and 100.");

            RuleFor(dto => dto.UpdatedCheckpoints)
                .NotEmpty().WithMessage("Updated checkpoints must not be empty.");

            RuleForEach(dto => dto.UpdatedCheckpoints).SetValidator(new CheckpointListDtoValidator());
        }
    }

    public class CheckpointListDtoValidator : AbstractValidator<CheckpointListDto>
    {
        public CheckpointListDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Checkpoint ID must not be empty.");

            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Checkpoint name must not be empty.");

            RuleFor(dto => dto.Score)
                .NotEmpty().WithMessage("Checkpoint score must not be empty.");
        }
    }
}
