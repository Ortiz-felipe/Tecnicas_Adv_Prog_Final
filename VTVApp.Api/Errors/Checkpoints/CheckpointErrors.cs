namespace VTVApp.Api.Errors.Checkpoints
{
    public static class CheckpointErrors
    {
        // Error when failing to get all checkpoints
        public static ApiError GetAllCheckpointsError =>
            new(MajorErrorCodes.Checkpoints, 1, "Error occurred while retrieving all checkpoints.");

        // Error when a specific checkpoint could not be found
        public static ApiError GetCheckpointNotFoundError(Guid checkpointId) =>
            new(MajorErrorCodes.Checkpoints, 2, $"Checkpoint with ID {checkpointId} could not be found.");

        // Error when there is a problem creating a new checkpoint
        public static ApiError CreateCheckpointError =>
            new(MajorErrorCodes.Checkpoints, 3, "Error occurred while creating a new checkpoint.");

        // Error when a checkpoint update fails
        public static ApiError UpdateCheckpointError =>
            new(MajorErrorCodes.Checkpoints, 4, "Error occurred while updating the checkpoint.");

        // Error when failing to get checkpoints for a specific inspection
        public static ApiError GetInspectionCheckpointsError(Guid inspectionId) =>
            new(MajorErrorCodes.Checkpoints, 5, $"Error occurred while retrieving checkpoints for inspection with ID {inspectionId}.");

        // Error when a checkpoint deletion fails
        public static ApiError DeleteCheckpointError =>
            new(MajorErrorCodes.Checkpoints, 6, "Error occurred while attempting to delete the checkpoint.");

        // Error when could not get checkpoints for a specific appointment
        public static ApiError GetCheckpointsByAppointmentError =>
            new(MajorErrorCodes.Checkpoints, 7, "Error occurred while retrieving checkpoints for the appointment.");

        // Error when failing to get required recheck checkpoints for a specific vehicle
        public static ApiError GetRecheckRequiredCheckpointsByVehicleError =>
            new(MajorErrorCodes.Checkpoints, 8, "Error occurred while retrieving checkpoints that need a recheck for the vehicle.");

        // Error when failing to get a specific checkpoint
        public static ApiError GetCheckpointError =>
            new(MajorErrorCodes.Checkpoints, 9, "Error occurred while retrieving the checkpoint.");

        // Error when a specific checkpoint list cou

        // Error when an unexpected error occurs
        public static ApiError CheckpointUnexpectedError =>
            new(MajorErrorCodes.Checkpoints, 99, "An unexpected error occurred in checkpoint operations.");
    }

}
