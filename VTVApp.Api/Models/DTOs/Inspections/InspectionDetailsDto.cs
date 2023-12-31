﻿namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class InspectionDetailsDto
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime InspectionDate { get; set; }
        public List<InspectionCheckpointDetailDto> Checkpoints { get; set; }
        public string OverallComments { get; set; } // Any general comments from the inspector
        public int Status { get; set; } // Detailed status information
    }
}
