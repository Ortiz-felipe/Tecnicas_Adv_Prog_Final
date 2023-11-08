namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class InspectionOutcomeDto
    {
        public bool IsInspected { get; set; } // Indicates whether the inspection has been completed.
        public string Result { get; set; } // Safe, In Observation, Unsafe
        // Other relevant inspection result details.
    }
}
