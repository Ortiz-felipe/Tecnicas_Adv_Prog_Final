using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Checkpoints;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Repositories
{
    public class CheckpointsRepository : ICheckpointRepository
    {
        private readonly VTVDataContext _dbContext;
        private readonly IMapper _mapper;

        public CheckpointsRepository(VTVDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CheckpointSummaryDto>> GetAllCheckpointsSummaryAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CheckpointSummaryDto>?> GetAllCheckpointsSummaryByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken)
        {
            var checkpoints = await _dbContext.Inspections
                .Include(inspection => inspection.Appointment)
                .Where(x => x.AppointmentId == appointmentId)
                .SelectMany(x => x.Checkpoints)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CheckpointSummaryDto>>(checkpoints);
        }

        public async Task<CheckpointDetailsDto?> GetCheckpointDetailAsync(Guid checkpointId, CancellationToken cancellationToken)
        {
            var checkpoint = await _dbContext
                .Checkpoints
                .FirstOrDefaultAsync(x => x.Id == checkpointId, cancellationToken);

            return _mapper.Map<CheckpointDetailsDto>(checkpoint);
        }

        public async Task<IEnumerable<CheckpointSummaryDto>> GetInspectionResultsByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken)
        {
            var checkpoints = await _dbContext.Inspections
                .Include(inspection => inspection.Appointment)
                .Include(inspection => inspection.Checkpoints)
                .Where(x => x.Appointment.VehicleId == vehicleId)
                .SelectMany(x => x.Checkpoints)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CheckpointSummaryDto>>(checkpoints);
        }

        public async Task<IEnumerable<RecheckRequiredCheckpointDto>> GetAllRecheckRequiredByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken)
        {
            var checkpoints = await _dbContext.Inspections
                .Include(inspection => inspection.Appointment)
                .Include(inspection => inspection.Checkpoints)
                .Where(x => x.Appointment.VehicleId == vehicleId)
                .SelectMany(x => x.Checkpoints)
                .Where(x => x.Score <= 5)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<RecheckRequiredCheckpointDto>>(checkpoints);
        }

        public async Task<CheckpointOperationResultDto> AddCheckpointAsync(CheckpointDetailsDto checkpoint, CancellationToken cancellationToken)
        {
            var checkpointEntity = _mapper.Map<Checkpoint>(checkpoint);

            await _dbContext.Checkpoints.AddAsync(checkpointEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CheckpointOperationResultDto>(checkpointEntity);
        }

        public async Task<CheckpointOperationResultDto> UpdateCheckpointAsync(CheckpointDetailsDto checkpoint, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<CheckpointOperationResultDto> DeleteCheckpointAsync(Guid checkpointId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
