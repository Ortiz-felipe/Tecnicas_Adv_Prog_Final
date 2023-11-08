namespace VTVApp.Api.Errors.Appointments
{
    public static class AppointmentErrors
    {
        // Error when failing to get all appointments
        public static ApiError GetAllAppointmentsError =>
            new(MajorErrorCodes.Appointments, 1, "Error occurred while retrieving all appointments.");

        // Error when a specific appointment could not be found
        public static ApiError GetAppointmentNotFoundError(Guid appointmentId) =>
            new(MajorErrorCodes.Appointments, 2, $"Appointment with ID {appointmentId} could not be found.");

        // Error when there is a problem creating a new appointment
        public static ApiError CreateAppointmentError =>
            new(MajorErrorCodes.Appointments, 3, "Error occurred while scheduling a new appointment.");

        // Error when an appointment update fails
        public static ApiError UpdateAppointmentError =>
            new(MajorErrorCodes.Appointments, 4, "Error occurred while updating the appointment details.");

        // Error when an appointment cancellation fails
        public static ApiError CancelAppointmentError =>
            new(MajorErrorCodes.Appointments, 5, "Error occurred while attempting to cancel the appointment.");

        // Error when an appointment cannot be cancelled due to business rules
        public static ApiError CancelAppointmentNotAllowedError =>
            new(MajorErrorCodes.Appointments, 6, "Cancellation of the appointment is not allowed as per the business rules.");

        // Error when failing to get appointments for a specific user
        public static ApiError GetUserAppointmentsError(Guid userId) =>
            new(MajorErrorCodes.Appointments, 7, $"Error occurred while retrieving appointments for user with ID {userId}.");

        // Error when failing to get appointments for a specific vehicle
        public static ApiError GetVehicleAppointmentsError(Guid vehicleId) =>
            new(MajorErrorCodes.Appointments, 8, $"Error occurred while retrieving appointments for vehicle with ID {vehicleId}.");

        // Error when failing to get an specific appointment
        public static ApiError GetAppointmentByIdError(Guid appointmentId) =>
            new(MajorErrorCodes.Appointments, 9, $"Error occurred while retrieving appointment with ID {appointmentId}.");

        // Error when failing to get available appointment slots
        public static ApiError GetAvailableSlotsError =>
            new(MajorErrorCodes.Appointments, 10, "Error occurred while retrieving available appointment slots.");

        // Error when failing to get all appointments that require recheck
        public static ApiError GetRecheckRequiredAppointmentsError =>
            new(MajorErrorCodes.Appointments, 11, "Error occurred while retrieving appointments that require recheck.");


        // Error when an unexpected error occurs
        public static ApiError AppointmentUnexpectedError =>
            new(MajorErrorCodes.Appointments, 99, "An unexpected error occurred in appointment operations.");
    }

}
