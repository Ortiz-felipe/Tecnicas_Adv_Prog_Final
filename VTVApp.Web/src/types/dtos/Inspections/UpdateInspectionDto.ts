import { CheckpointListDto } from "../Checkpoints/CheckpointListDto";

export interface UpdateInspectionDto {
    id: string,
    appointmentId: string,
    result: string,
    totalScore: number,
    updatedCheckpoints: CheckpointListDto[]
}