import { CityDetailsDto } from "../types/dtos/Cities/CityDetailsDto";
import appInsights from "../utils/appInsights";

export const getCitiesByProvince = async (
  provinceId: number
): Promise<CityDetailsDto[]> => {
  const url = `${import.meta.env.VITE_API_URL}/api/Cities?ProvinceId=${provinceId}`
  const response = await fetch(url);

  if (!response.ok) {
    const error = await response.json();
    appInsights.trackException({ exception: error });
    throw new Error(error.detail || "Failed to fetch cities");
  }

  const cities: CityDetailsDto[] = await response.json();
  return cities;
};
