namespace VTVApp.Api.Errors.Provinces
{
    public static class ProvincesErrors
    {
        // Error when failing to get all provinces
        public static ApiError GetAllProvincesError =>
            new(MajorErrorCodes.Provinces, 1, "Error occurred while retrieving all provinces.");

        // Error when a specific province could not be found
        public static ApiError GetProvinceNotFoundError(int provinceId) =>
            new(MajorErrorCodes.Provinces, 2, $"Province with ID {provinceId} could not be found.");

        // Error when there is a problem creating a new province
        public static ApiError CreateProvinceError =>
            new(MajorErrorCodes.Provinces, 3, "Error occurred while creating a new province.");

        // Error when a province update fails
        public static ApiError UpdateProvinceError =>
            new(MajorErrorCodes.Provinces, 4, "Error occurred while updating the province information.");

        // Error when a province deletion fails
        public static ApiError DeleteProvinceError =>
            new(MajorErrorCodes.Provinces, 5, "Error occurred while attempting to delete the province.");

        // Error when a province cannot be deleted due to related data
        public static ApiError DeleteProvinceConflictError =>
            new(MajorErrorCodes.Provinces, 6, "Province cannot be deleted due to related entities.");

        // Error when an unexpected error occurs
        public static ApiError ProvinceUnexpectedError =>
            new(MajorErrorCodes.Provinces, 99, "An unexpected error occurred in the province operations.");
    }

}
