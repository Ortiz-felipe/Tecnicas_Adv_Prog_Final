using FluentValidation;
using System.Globalization;

namespace VTVApp.Api.Queries.Appointments.GetAvailableSlots
{
    public class ValidatorCollection : AbstractValidator<GetAvailableSlotsQuery>
    {
        public ValidatorCollection()
        {
            this.ClassLevelCascadeMode = CascadeMode.Stop;

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .Must(BeAValidDate).WithMessage("Invalid date format. Please use the format 'YYYY-MM-dd'.")
                .Must(BeAValidFutureDate).WithMessage("The date cannot be a past date.");
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private bool BeAValidFutureDate(string date)
        {
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return parsedDate >= DateTime.UtcNow.Date;
            }
            return false;
        }
    }
}
