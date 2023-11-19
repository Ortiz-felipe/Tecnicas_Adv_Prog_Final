import { UserDto } from "../Users/UserDto";
import { VehicleDto } from "../Vehicles/VehicleDto";

export interface AppointmentDetailsDto {
    id: string,
    appointmentDate: string, 
    user: UserDto,
    vehicle: VehicleDto
}