using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Vehicle;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        /// <summary>
        /// Retrieves a vehicle by its ID.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
        /// <returns>A Task containing the VehicleDto.</returns>
        Task<VehicleDto?> GetVehicleByIdAsync(Guid vehicleId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a list of all vehicles.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
        /// <returns>A Task containing an IEnumerable of VehicleDto.</returns>
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a list of vehicles for a specified user.
        /// </summary>
        /// <param name="userId">The ID of the user whose vehicles we will retrieve</param>
        /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
        /// <returns>A Task containing an IEnumerable of VehicleDto.</returns>
        Task<IEnumerable<VehicleDto>?> GetVehiclesByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="createVehicleDto">The vehicle data transfer object containing information for the creation.</param>
        /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
        /// <returns>A Task containing the VehicleOperationResultDto with the result of the create operation.</returns>
        Task<VehicleOperationResultDto> AddVehicleAsync(CreateVehicleDto createVehicleDto, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the details of an existing vehicle.
        /// </summary>
        /// <param name="updateVehicleDto">The vehicle data transfer object containing the updated information.</param>
        /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
        /// <returns>A Task containing the VehicleOperationResultDto with the result of the update operation.</returns>
        Task<VehicleOperationResultDto> UpdateVehicleAsync(VehicleUpdateDto updateVehicleDto, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a vehicle from the repository.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle to remove.</param>
        /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
        /// <returns>A Task containing the OperationResultDto indicating the success or failure of the removal.</returns>
        Task<OperationResultDto> DeleteVehicleAsync(Guid vehicleId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a list of vehicles based on a specified filter.
        /// </summary>
        /// <param name="vehicleFilterDto">The data transfer object containing filter criteria.</param>
        /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
        /// <returns>A Task containing an IEnumerable of VehicleDto that match the filter criteria.</returns>
        Task<IEnumerable<VehicleDto>> FindVehiclesAsync(VehicleFilterDto vehicleFilterDto, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a vehicle marked as favorite for a specified user.
        /// </summary>
        /// <param name="userId">The ID of the User to fetch his/her favorite vehicle</param>
        /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
        /// <returns>A Task containing the VehicleDto.</returns>
        Task<VehicleDto?> GetFavoriteVehicleByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }

}
