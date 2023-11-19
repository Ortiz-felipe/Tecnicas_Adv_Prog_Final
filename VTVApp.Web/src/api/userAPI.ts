// src/api/userAPI.ts
import { UserAuthenticationResultDto } from '../types/dtos/Users/UserAuthenticationResultDto';
import { UserOperationResultDto } from '../types/dtos/Users/UserOperationResultDto';
import { UserRegistartionDto } from '../types/dtos/Users/UserRegistrationDto';
const baseURL = import.meta.env.VITE_API_URL

interface LoginCredentials {
  email: string;
  password: string;
}

export const loginUser = async (credentials: LoginCredentials): Promise<UserAuthenticationResultDto> => {
  try {
    const url = `${baseURL}/api/Users/login`;
    const requestConfig: RequestInit = {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        body: credentials
      }),
    }
    const response = await fetch(url, requestConfig);

    if (!response.ok) {
      // Attempt to parse the error message from the response, default to 'Failed to login'
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage || 'Failed to login');
    }

    // If the response is ok, parse and return the data
    const data: UserAuthenticationResultDto = await response.json();
    return data;
  } catch (error: any) {
    // Rethrow any caught errors as a new error to be handled by the caller
    throw new Error(error.message || 'An unexpected error occurred');
  }
};

export const registerUser = async (credentials: UserRegistartionDto): Promise<UserOperationResultDto> => {
  try {
    const url = `${baseURL}/api/Users`;
    const requestConfig: RequestInit = {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        body: credentials
      }),
    };
    const response = await fetch(url, requestConfig);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage || 'Failed to register');
    }

    const data: UserOperationResultDto = await response.json();
    return data;
  } catch (error: any) {
    throw new Error(error.message || 'An unexpected error occurred during registration');
  }
};