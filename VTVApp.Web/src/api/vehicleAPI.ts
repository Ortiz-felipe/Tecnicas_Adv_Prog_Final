import { CreateVehicleDto } from "../types/dtos/Vehicles/CreateVehicleDto";
import { UpdateVehicleDto } from "../types/dtos/Vehicles/UpdateVehicleDto";
import { VehicleDto } from "../types/dtos/Vehicles/VehicleDto";
import { VehicleOperationResultDto } from "../types/dtos/Vehicles/VehicleOperationResultDto";
import appInsights from "../utils/appInsights";

const baseURL = import.meta.env.VITE_API_URL;

export const getFavoriteVehicleForUserId = async (
  userId: string
): Promise<VehicleDto | null> => {
  try {
    const url = `${baseURL}/api/Vehicles/user/${userId}/favorite`;
    const response = await fetch(url);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    if (response.status === 200) {
      const data: VehicleDto = await response.json();
      return data;
    }

    return null;
  } catch (error: any) {
    appInsights.trackException({ exception: error });
    throw new Error(error.message);
  }
};

export const getAllVehiclesForUserId = async (
  userId: string
): Promise<VehicleDto[]> => {
  try {
    const url = `${baseURL}/api/Vehicles/user/${userId}`;
    const response = await fetch(url);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data: VehicleDto[] = await response.json();
    return data;
  } catch (error: any) {
    appInsights.trackException({ exception: error });
    throw new Error(error.message);
  }
};

export const markVehicleAsFavorite = async (
  vehicleData: UpdateVehicleDto,
  vehicleId: string
): Promise<VehicleOperationResultDto> => {
  try {
    const url = `${baseURL}/api/Vehicles/${vehicleId}`;
    const requestConfig: RequestInit = {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        vehicleId: vehicleId,
        body: vehicleData,
      }),
    };

    const response = await fetch(url, requestConfig);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage || "Failed to mark vehicle as favorite");
    }

    // If the response is ok, parse and return the data
    const data: VehicleOperationResultDto = await response.json();
    return data;
  } catch (error: any) {
    appInsights.trackException({ exception: error });
    throw new Error(error.message);
  }
};

export const createNewVehicle = async (vehicleData : CreateVehicleDto) : Promise<VehicleOperationResultDto> => {
  try {
    const url = `${baseURL}/api/Vehicles`;
    const requestConfig : RequestInit = {
      method: 'POST',
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        body: vehicleData,
      })
    }

    const response = await fetch(url, requestConfig);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage || "Failed to add a new vehicle");
    }

    const data: VehicleOperationResultDto = await response.json();
    return data;
  } catch (error : any) {
    appInsights.trackException({ exception: error });
    throw new Error(error.message)
  }
}
