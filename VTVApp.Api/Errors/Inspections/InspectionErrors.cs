namespace VTVApp.Api.Errors.Inspections
{
    public static class InspectionErrors
    {
        // Error when failing to retrieve all inspections
        public static ApiError GetAllInspectionsError =>
            new(MajorErrorCodes.Inspections, 1, "Error occurred while retrieving all inspections.");

        // Error when an inspection with the specified ID could not be found
        public static ApiError GetInspectionNotFoundError(Guid inspectionId) =>
            new(MajorErrorCodes.Inspections, 2, $"Inspection with ID {inspectionId} could not be found.");

        // Error when there is an issue creating a new inspection
        public static ApiError CreateInspectionError =>
            new(MajorErrorCodes.Inspections, 3, "Error occurred while creating a new inspection.");

        // Error when an inspection update fails
        public static ApiError UpdateInspectionError =>
            new(MajorErrorCodes.Inspections, 4, "Error occurred while updating the inspection.");

        // Error when failing to retrieve inspection details
        public static ApiError GetInspectionDetailsError(Guid inspectionId) =>
            new(MajorErrorCodes.Inspections, 5, $"Error occurred while retrieving details for inspection with ID {inspectionId}.");

        // Error when an error occurs during inspection deletion
        public static ApiError DeleteInspectionError =>
            new(MajorErrorCodes.Inspections, 6, "Error occurred while attempting to delete the inspection.");

        // Error when failing to get inspections for a specific vehicle
        public static ApiError GetVehicleInspectionsError(Guid vehicleId) =>
            new(MajorErrorCodes.Inspections, 7, $"Error occurred while retrieving inspections for vehicle with ID {vehicleId}.");

        // Error when failing to get inspections requiring recheck
        public static ApiError GetRecheckRequiredInspectionsError =>
            new(MajorErrorCodes.Inspections, 8, "Error occurred while retrieving inspections requiring recheck.");

        // Error for any unexpected issue during inspection operations
        public static ApiError InspectionUnexpectedError =>
            new(MajorErrorCodes.Inspections, 99, "An unexpected error occurred in inspection operations.");
    }

}
