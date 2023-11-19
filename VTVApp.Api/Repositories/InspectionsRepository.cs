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
                .Where(i => i.Appointment.Status == AppointmentStatus.Pending)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<InspectionListDto>>(inspections);
        }

        public async Task<IEnumerable<InspectionListDto>> GetInspectionsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var inspections = await _context.Inspections
                .Include(i => i.Appointment)
                .ThenInclude(a => a.Vehicle)
                .Include(i => i.Checkpoints)
                .Where(i => i.Appointment.Vehicle.UserId == userId)
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

        public async Task<InspectionDetailsDto?> GetLatestInspectionByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken)
        {
            var inspection = await _context.Inspections
                .Include(i => i.Appointment)
                .Include(i => i.Checkpoints)
                .Where(i => i.Appointment.VehicleId == vehicleId)
                .OrderByDescending(i => i.Appointment.Date)
                .FirstOrDefaultAsync(cancellationToken);

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

        public async Task<InspectionOperationResultDto?> AddInspectionAsync(CreateInspectionDto inspection, CancellationToken cancellationToken)
        {
            var newInspection = _mapper.Map<Inspection>(inspection);
            newInspection.Status = InspectionStatus.InProgress;

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

            if (!inspectionToUpdate.Checkpoints.Any())
            {
                var newCheckpoints = inspection.UpdatedCheckpoints.Select(ucp => new Checkpoint()
                {
                    Name = ucp.Name,
                    Score = ucp.Score,
                    Comment = ucp.Comment,
                    InspectionId = inspectionToUpdate.Id
                }).ToList();

                newCheckpoints.ForEach(c => inspectionToUpdate.Checkpoints.Add(c));
                await _context.Checkpoints.AddRangeAsync(newCheckpoints, cancellationToken);
            }
            else
            {
                // Update checkpoints
                foreach (var updatedCheckpointDto in inspection.UpdatedCheckpoints)
                {
                    var checkpoint = inspectionToUpdate.Checkpoints
                        .SingleOrDefault(cp => cp.Id == updatedCheckpointDto.Id);

                    if (checkpoint != null)
                    {
                        // Update existing checkpoint
                        checkpoint.Name = updatedCheckpointDto.Name;
                        checkpoint.Score = updatedCheckpointDto.Score;
                        checkpoint.Comment = updatedCheckpointDto.Comment;
                    }
                }
            }

            if (inspectionToUpdate.Checkpoints.Any(c => c.Score <= 5))
            {
                inspectionToUpdate.Status = InspectionStatus.CompletedFailed; 
            }

            switch (inspectionToUpdate.TotalScore)
            {
                case < 40:
                    inspectionToUpdate.Status = InspectionStatus.CompletedFailed; 
                    break;
                case >= 80:
                    inspectionToUpdate.Status = InspectionStatus.CompletedApproved; 
                    break;
                default:
                    inspectionToUpdate.Status = InspectionStatus.InReview; 
                    break;
            }

            // Update appointment status to completed
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == inspectionToUpdate.AppointmentId, cancellationToken);
            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.Completed;
            }

            // Save changes
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle the concurrency exception
                // Log the exception details and/or rethrow as a business exception
                throw;
            }

            return new InspectionOperationResultDto
            {
                Success = true,
                Message = "Inspection updated successfully.",
                InspectionDetails = _mapper.Map<InspectionDetailsDto>(inspectionToUpdate)
            };
        }

        public async Task<OperationResultDto> DeleteInspectionAsync(Guid inspectionId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
