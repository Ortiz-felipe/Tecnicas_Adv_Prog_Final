import { CreateInspectionDto } from "../types/dtos/Inspections/CreateInspectionDto";
import { InspectionDetailsDto } from "../types/dtos/Inspections/InspectionDetailsDto";
import { InspectionListDto } from "../types/dtos/Inspections/InspectionListDto";
import { InspectionOperationResultDto } from "../types/dtos/Inspections/InspectionOperationResultDto";
import { UpdateInspectionDto } from "../types/dtos/Inspections/UpdateInspectionDto";

const baseURL = import.meta.env.VITE_API_URL;

export const getLatestVehicleInspection = async (vehicleId : string) : Promise<InspectionDetailsDto> => {
    try {
        const url = `${baseURL}/api/Inspections/vehicle/${vehicleId}/latest`;
        const response = await fetch(url);
    
        if (!response.ok) {
          const errorData: { errorMessage?: string } = await response.json();
          throw new Error(errorData.errorMessage);
        }
    
        const data : InspectionDetailsDto = await response.json();
        return data;
      } catch (error : any) {
        throw new Error(error.message);
      }
}

export const getAllInspections = async (userId : string) : Promise<InspectionListDto[]> => {
  try {
    const url = `${baseURL}/api/Inspections/user/${userId}`;
    const response = await fetch(url);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data : InspectionListDto[] = await response.json();
    return data;
  } catch (error : any) {
    throw new Error(error.message);
  }
}

export const getInspectionDetails = async (inspectionId : string) : Promise<InspectionDetailsDto> => {
  try {
    const url = `${baseURL}/api/Inspections/${inspectionId}`;
    const response = await fetch(url);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data : InspectionDetailsDto = await response.json();
    return data;
  } catch (error : any) {
    throw new Error(error.message);
  }
}

export const createInspection = async (newInspection: CreateInspectionDto, appointmentId : string) : Promise<InspectionOperationResultDto> => {
  try {
    const url = `${baseURL}/api/Inspections/${appointmentId}`;
    const requestConfig : RequestInit = {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        appointmentId: appointmentId,
        body: newInspection
      }),
    }

    const response = await fetch(url, requestConfig);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data : InspectionOperationResultDto = await response.json();
    return data;
  } catch (error : any) {
    throw new Error(error.message);
  }
}

export const saveInspectionResults = async (inspectionData: UpdateInspectionDto, inspectionId : string) : Promise<InspectionOperationResultDto> => {
  try {
    const url = `${baseURL}/api/Inspections/${inspectionId}/complete`;
    const requestConfig : RequestInit = {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        inspectionId: inspectionId,
        body: inspectionData
      }),
    }

    const response = await fetch(url, requestConfig);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data : InspectionOperationResultDto = await response.json();
    return data;
  } catch (error : any) {
    throw new Error(error.message);
  }
}