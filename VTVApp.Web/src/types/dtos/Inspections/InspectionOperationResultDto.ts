import { InspectionDetailsDto } from "./InspectionDetailsDto";

export interface InspectionOperationResultDto {
    success: boolean,
    message: string,
    inspectionDetails: InspectionDetailsDto
}