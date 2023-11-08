namespace VTVApp.Api.Errors.Cities
{
    public static class CitiesErrors
    {
        // Error when failing to get all cities
        public static ApiError GetAllCitiesError =>
            new(MajorErrorCodes.Cities, 1, "Error occurred while retrieving all cities.");

        // Error when a specific city could not be found
        public static ApiError GetCityNotFoundError(int cityId) =>
            new(MajorErrorCodes.Cities, 2, $"City with ID {cityId} could not be found.");

        // Error when there is a problem creating a new city
        public static ApiError CreateCityError =>
            new(MajorErrorCodes.Cities, 3, "Error occurred while creating a new city.");

        // Error when a city update fails
        public static ApiError UpdateCityError =>
            new(MajorErrorCodes.Cities, 4, "Error occurred while updating the city information.");

        // Error when a city deletion fails
        public static ApiError DeleteCityError =>
            new(MajorErrorCodes.Cities, 5, "Error occurred while attempting to delete the city.");

        // Error when a city cannot be deleted due to related data
        public static ApiError DeleteCityConflictError =>
            new(MajorErrorCodes.Cities, 6, "City cannot be deleted due to related entities.");

        // Error when attempting to retrieve cities by province and it fails
        public static ApiError GetCitiesByProvinceError =>
            new(MajorErrorCodes.Cities, 7, "Error occurred while retrieving cities for the specified province.");

        // Error when an unexpected error occurs
        public static ApiError CityUnexpectedError =>
            new(MajorErrorCodes.Cities, 99, "An unexpected error occurred in city operations.");
    }

}
