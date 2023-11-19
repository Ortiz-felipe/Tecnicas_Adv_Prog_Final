import { InspectionCheckpointDetailDto } from "./InspectionCheckpointDetailDto";

export interface InspectionListDto {
  id: string;
  vehicleId: string;
  licensePlate: string;
  inspectionDate: string;
  checkpoints: InspectionCheckpointDetailDto[]
  status: number;
}
