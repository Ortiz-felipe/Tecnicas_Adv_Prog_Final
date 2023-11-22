import { ProvinceDto } from "../types/dtos/Provinces/ProvinceDto";
import appInsights from "../utils/appInsights";

const baseURL = import.meta.env.VITE_API_URL;

const getProvinces = async (): Promise<ProvinceDto[]> => {
  try {
    const url = `${baseURL}/api/Provinces`;
    const response = await fetch(url);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data : ProvinceDto[] = await response.json();
    return data;
  } catch (error : any) {
    appInsights.trackException({ exception: error });
    throw new Error(error.message);
  }
};

export default getProvinces;
