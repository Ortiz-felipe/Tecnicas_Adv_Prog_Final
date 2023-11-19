import { InspectionCheckpointDetailDto } from "./InspectionCheckpointDetailDto"

export interface InspectionDetailsDto {
    id: string,
    vehicleId: string, 
    inspectorName: string,
    inspectionDate: string,
    checkpoints: InspectionCheckpointDetailDto[]
    overallComments: string,
    status: number
}