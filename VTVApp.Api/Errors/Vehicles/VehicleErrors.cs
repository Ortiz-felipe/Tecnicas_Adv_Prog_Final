namespace VTVApp.Api.Errors.Vehicles
{
    public static class VehicleErrors
    {
        // Error when retrieving all vehicles fails
        public static ApiError GetAllVehiclesError =>
            new(MajorErrorCodes.Vehicles, 1, "Error occurred while retrieving all vehicles.");

        // Error when a specific vehicle cannot be found
        public static ApiError GetVehicleNotFoundError(Guid vehicleId) =>
            new(MajorErrorCodes.Vehicles, 2, $"Vehicle with ID {vehicleId} could not be found.");

        // Error when creating a new vehicle fails
        public static ApiError CreateVehicleError =>
            new(MajorErrorCodes.Vehicles, 3, "Error occurred while creating a new vehicle.");

        // Error when updating a vehicle fails
        public static ApiError UpdateVehicleError =>
            new(MajorErrorCodes.Vehicles, 4, "Error occurred while updating the vehicle.");

        // Error when deleting a vehicle fails
        public static ApiError DeleteVehicleError =>
            new(MajorErrorCodes.Vehicles, 5, "Error occurred while attempting to delete the vehicle.");

        // Error when a vehicle registration fails
        public static ApiError VehicleRegistrationFailedError =>
            new(MajorErrorCodes.Vehicles, 6, "Vehicle registration failed due to an error.");

        // Error when a vehicle inspection fails
        public static ApiError VehicleInspectionFailedError =>
            new(MajorErrorCodes.Vehicles, 7, "Vehicle inspection failed due to an error.");

        // Error when a vehicle's details update fails
        public static ApiError VehicleDetailsUpdateError =>
            new(MajorErrorCodes.Vehicles, 8, "Error occurred while updating vehicle details.");

        // Error when a vehicle's ownership transfer fails
        public static ApiError VehicleTransferFailedError =>
            new(MajorErrorCodes.Vehicles, 9, "Vehicle ownership transfer failed.");

        // Error when retrieving a vehicle by ID fails
        public static ApiError GetVehicleByIdError =>
            new(MajorErrorCodes.Vehicles, 10, "Error occurred while retrieving vehicle by ID.");

        // Error when retrieving a list of vehicles by user ID fails
        public static ApiError GetVehiclesByUserIdError =>
            new(MajorErrorCodes.Vehicles, 11, "Error occurred while retrieving vehicles by user ID.");

        // Error when retrieving a list of vehicles for an non-existing user
        public static ApiError GetVehiclesForNonExistingUserError =>
            new(MajorErrorCodes.Vehicles, 12, "Error occurred while retrieving vehicles for a non-existing user.");

        // Error when retrieving a favorite vehicle for a user fails
        public static ApiError GetFavoriteVehicleByUserIdError =>
            new(MajorErrorCodes.Vehicles, 13, "Error occurred while retrieving favorite vehicle by user ID.");

        //Error when user has no favorite vehicle
        public static ApiError UserHasNoFavoriteVehicleError =>
            new(MajorErrorCodes.Vehicles, 14, "User has no favorite vehicle.");

        // Error for any unexpected issue during vehicle operations
        public static ApiError VehicleUnexpectedError =>
            new(MajorErrorCodes.Vehicles, 99, "An unexpected error occurred in vehicle operations.");
    }

}
