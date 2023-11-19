import { CheckpointListDto } from "../Checkpoints/CheckpointListDto";

export interface CreateInspectionDto {
    vehicleId: string,
    inspectionDate: string,
    result: number,
    appointmentId: string,
    checkpoints: CheckpointListDto[]
}