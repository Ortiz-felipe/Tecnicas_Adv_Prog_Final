import { UserDto } from "./UserDto";

export interface UserAuthenticationResultDto {
    isAuthenticated: boolean,
    token: string,
    user: UserDto,
    errorMessage: string
}