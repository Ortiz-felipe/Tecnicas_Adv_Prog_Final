using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Repositories
{
    public class InspectionsRepository : IInspectionRepository
    {
        private readonly VTVDataContext _context;
        private readonly IMapper _mapper;

        public InspectionsRepository(VTVDataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<InspectionListDto>> GetAllInspectionsAsync(CancellationToken cancellationToken)
        {
            var inspections = await _context.Inspections
                .Include(i => i.Appointment)
                .Include(i => i.Checkpoints)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<InspectionListDto>>(inspections);
        }

        public async Task<InspectionDetailsDto?> GetInspectionByIdAsync(Guid inspectionId, CancellationToken cancellationToken)
        {
            var inspection = await _context.Inspections
                .Include(i => i.Appointment)
                .Include(i => i.Checkpoints)
                .FirstOrDefaultAsync(i => i.Id == inspectionId, cancellationToken);

            return _mapper.Map<InspectionDetailsDto>(inspection);
        }

        public async Task<IEnumerable<InspectionListDto>?> GetInspectionsByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken)
        {
            var inspections = await _context.Inspections
                .Include(i => i.Appointment)
                .Include(i => i.Checkpoints)
                .Where(i => i.Appointment.VehicleId == vehicleId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<InspectionListDto>>(inspections);
        }

        public async Task<IEnumerable<InspectionListDto>> GetInspectionsByRecheckRequiredAsync(CancellationToken cancellationToken)
        {
            var inspections = await _context.Inspections
                .Include(i => i.Appointment)
                .Include(i => i.Checkpoints)
                .Where(inspection => inspection.TotalScore <= 40 || inspection.Checkpoints.Any(checkpoint => checkpoint.Score <= 5))
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<InspectionListDto>>(inspections);
        }

        public async Task<InspectionOperationResultDto> AddInspectionAsync(CreateInspectionDto inspection, CancellationToken cancellationToken)
        {
            var newInspection = _mapper.Map<Inspection>(inspection);

            await _context.Inspections.AddAsync(newInspection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<InspectionOperationResultDto>(newInspection);
        }

        public async Task<InspectionOperationResultDto> UpdateInspectionAsync(UpdateInspectionDto inspection, CancellationToken cancellationToken)
        {
            // Find the existing inspection
            var inspectionToUpdate = await _context.Inspections
                .Include(i => i.Checkpoints)
                .FirstOrDefaultAsync(i => i.Id == inspection.Id, cancellationToken);

            if (inspectionToUpdate == null)
            {
                return new InspectionOperationResultDto
                {
                    Success = false,
                    Message = "Inspection not found."
                };
            }

            // Update the inspection properties
            inspectionToUpdate.Result = inspection.Result;
            inspectionToUpdate.TotalScore = inspection.TotalScore;

            // Update each checkpoint
            foreach (var updatedCheckpoint in inspection.UpdatedCheckpoints)
            {
                var checkpoint = inspectionToUpdate.Checkpoints.FirstOrDefault(cp => cp.Id == updatedCheckpoint.Id);
                if (checkpoint != null)
                {
                    checkpoint.Score = updatedCheckpoint.Score;
                    checkpoint.Comment = updatedCheckpoint.Comment;
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            // Map the updated inspection to InspectionDetailsDto
            var inspectionDetailsDto = _mapper.Map<InspectionDetailsDto>(inspection);

            // Return the operation result
            return new InspectionOperationResultDto
            {
                Success = true,
                Message = "Inspection updated successfully.",
                InspectionDetails = inspectionDetailsDto
            };
        }

        public async Task<OperationResultDto> DeleteInspectionAsync(Guid inspectionId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
