using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Repositories.Interfaces
{
    public interface IInspectionRepository
    {
        /// <summary>
        /// Retrieves all inspections asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token for canceling the operation if necessary.</param>
        /// <returns>A collection of InspectionListDto.</returns>
        Task<IEnumerable<InspectionListDto>> GetAllInspectionsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a specific inspection by its ID asynchronously.
        /// </summary>
        /// <param name="inspectionId">The ID of the inspection.</param>
        /// <param name="cancellationToken">A token for canceling the operation if necessary.</param>
        /// <returns>An InspectionDetailsDto containing the detailed information of the inspection.</returns>
        Task<InspectionDetailsDto?> GetInspectionByIdAsync(Guid inspectionId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a list of inspections associated with a specific vehicle ID asynchronously.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle associated with the inspections.</param>
        /// <param name="cancellationToken">A token for canceling the operation if necessary.</param>
        /// <returns>A collection of InspectionListDto.</returns>
        Task<IEnumerable<InspectionListDto>?> GetInspectionsByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a list of inspections that require recheck asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token for canceling the operation if necessary.</param>
        /// <returns>A collection of InspectionListDto.</returns>
        Task<IEnumerable<InspectionListDto>> GetInspectionsByRecheckRequiredAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new inspection asynchronously.
        /// </summary>
        /// <param name="inspection">The data transfer object containing the information needed to create a new inspection.</param>
        /// <param name="cancellationToken">A token for canceling the operation if necessary.</param>
        /// <returns>An InspectionOperationResultDto indicating the result of the create operation.</returns>
        Task<InspectionOperationResultDto> AddInspectionAsync(CreateInspectionDto inspection, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing inspection asynchronously.
        /// </summary>
        /// <param name="inspection">The data transfer object containing the information needed to update the inspection.</param>
        /// <param name="cancellationToken">A token for canceling the operation if necessary.</param>
        /// <returns>An InspectionOperationResultDto indicating the result of the update operation.</returns>
        Task<InspectionOperationResultDto> UpdateInspectionAsync(UpdateInspectionDto inspection, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a specific inspection by its ID asynchronously.
        /// </summary>
        /// <param name="inspectionId">The ID of the inspection to be deleted.</param>
        /// <param name="cancellationToken">A token for canceling the operation if necessary.</param>
        /// <returns>An OperationResultDto indicating the result of the delete operation.</returns>
        Task<OperationResultDto> DeleteInspectionAsync(Guid inspectionId, CancellationToken cancellationToken);
    }

}
