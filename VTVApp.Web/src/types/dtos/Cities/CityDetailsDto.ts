import { ProvinceDto } from "../Provinces/ProvinceDto";

export interface CityDetailsDto {
  id: string;
  name: string;
  postalCode: number;
  province: ProvinceDto;
}
