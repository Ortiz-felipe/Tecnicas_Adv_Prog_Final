using VTVApp.Api.Models.DTOs.Checkpoints;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Repositories.Interfaces
{
    public interface ICheckpointRepository
    {
        /// <summary>
        /// Retrieves all checkpoints in a summary form.
        /// </summary>
        /// <returns>A collection of CheckpointSummaryDto objects.</returns>
        Task<IEnumerable<CheckpointSummaryDto>> GetAllCheckpointsSummaryAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all checkpoints in a summary form for a specific appointment.
        /// </summary>
        /// <param name="appointmentId">The unique identifier of an appointment.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of CheckpointSummaryDto objects.</returns>
        Task<IEnumerable<CheckpointSummaryDto>?> GetAllCheckpointsSummaryByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a specific checkpoint's details by its ID.
        /// </summary>
        /// <param name="checkpointId">The unique identifier of the checkpoint.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The CheckpointDetailDto if found; otherwise, null.</returns>
        Task<CheckpointDetailsDto?> GetCheckpointDetailAsync(Guid checkpointId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all checkpoints results in a summary form for a specific vehicle.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of checkpoints that need to be rechecked of a vehicle</returns>
        Task<IEnumerable<CheckpointSummaryDto>> GetInspectionResultsByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all checkpoints that need a recheck for a specific vehicle.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of checkpoints that need to be rechecked of a vehicle</returns>
        Task<IEnumerable<RecheckRequiredCheckpointDto>> GetAllRecheckRequiredByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new checkpoint to the repository.
        /// </summary>
        /// <param name="checkpoint">The CheckpointDetailDto containing the details of the checkpoint to add.</param>
        /// <param name = "cancellationToken" > The cancellation token.</param>
        /// <returns>The operation result including the created checkpoint's details.</returns>
        Task<CheckpointOperationResultDto> AddCheckpointAsync(CheckpointDetailsDto checkpoint, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the details of an existing checkpoint.
        /// </summary>
        /// <param name="checkpoint">The CheckpointDetailDto containing the updated details of the checkpoint.</param>
        /// <returns>The operation result including the updated checkpoint's details.</returns>
        Task<CheckpointOperationResultDto> UpdateCheckpointAsync(CheckpointDetailsDto checkpoint, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a checkpoint from the repository.
        /// </summary>
        /// <param name="checkpointId">The unique identifier of the checkpoint to remove.</param>
        /// <returns>The operation result indicating the success or failure of the removal.</returns>
        Task<CheckpointOperationResultDto> DeleteCheckpointAsync(Guid checkpointId, CancellationToken cancellationToken);
    }

}
