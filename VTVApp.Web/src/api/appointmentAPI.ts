import { StatusCodes } from "http-status-codes";
import { AppointmentDetailsDto } from "../types/dtos/Appointments/AppointmentDetailsDto";
import { AppointmentListDto } from "../types/dtos/Appointments/AppointmentListDto";
import { AvailableAppointmentSlotsDto } from "../types/dtos/Appointments/AvailableAppointmentSlotDto";
import { CreateAppointmentDto } from "../types/dtos/Appointments/CreateAppointmentDto";
import appInsights from "../utils/appInsights";

const baseURL = import.meta.env.VITE_API_URL;

export const getAllAppointments = async (): Promise<AppointmentListDto[]> => {
  try {
    const url = `${baseURL}/api/Appointments`;
    const response = await fetch(url);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data: AppointmentListDto[] = await response.json();
    return data;
  } catch (error: any) {
    appInsights.trackException({ exception: error });
    throw new Error(error.message);
  }
};

export const getAppointmentById = async (
  appointmentId: string
): Promise<AppointmentDetailsDto> => {
  try {
    const url = `${baseURL}/api/Appointments/${appointmentId}`;
    const response = await fetch(url);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data: AppointmentDetailsDto = await response.json();
    return data;
  } catch (error: any) {
    appInsights.trackException({ exception: error });
    throw new Error(error.message);
  }
};

export const getLatestAppointment = async (
  userId: string
): Promise<AppointmentDetailsDto | null> => {
  try {
    const url = `${baseURL}/api/Appointments/latest/${userId}`;
    const response = await fetch(url);

    if (response.status === StatusCodes.NO_CONTENT) {
      return null;
    }

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data: AppointmentDetailsDto = await response.json();
    return data;
  } catch (error: any) {
    appInsights.trackException({ exception: error });
    throw new Error(error.message);
  }
};

export const getAllAppointmentsForUser = async (
  userId: string
): Promise<AppointmentListDto[]> => {
  try {
    const url = `${baseURL}/api/Appointments/user/${userId}`;
    const response = await fetch(url);

    switch (response.status) {
      case StatusCodes.OK:
        // If status code is 200 OK, parse the JSON response and return it
        const appointments: AppointmentListDto[] = await response.json();
        return appointments;

      case StatusCodes.NO_CONTENT:
        // If status code is 204 No Content, return an empty array
        return [];

      case StatusCodes.INTERNAL_SERVER_ERROR:
        const errorData: { errorMessage?: string } = await response.json();
        throw new Error(errorData.errorMessage);

      default:
        // Handle any other unexpected status codes
        throw new Error(`Unexpected response status: ${response.status}`);
    }
  } catch (error : any) {
    appInsights.trackException({ exception: error });
    // Log the error or handle it as needed
    console.error("Error fetching appointments:", error);
    throw error; // Re-throw the error to let the caller handle it
  }
};

export const getAvailableTimeSlotsForSelectedDate = async (
  selectedDate: string
): Promise<AvailableAppointmentSlotsDto> => {
  try {
    const url = `${baseURL}/api/Appointments/availableSlots/${selectedDate}`;
    const response = await fetch(url);

    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage);
    }

    const data: AvailableAppointmentSlotsDto = await response.json();
    return data;
  } catch (error: any) {
    appInsights.trackException({ exception: error });
    throw new Error(error.message);
  }
};

export const createNewAppointment = async (
  appointmentData: CreateAppointmentDto
): Promise<AppointmentDetailsDto> => {
  try {
    const url = `${baseURL}/api/Appointments`;
    const requestConfig: RequestInit = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        body: appointmentData,
      }),
    };

    const response = await fetch(url, requestConfig);
    if (!response.ok) {
      const errorData: { errorMessage?: string } = await response.json();
      throw new Error(errorData.errorMessage || "Failed to login");
    }

    // If the response is ok, parse and return the data
    const data: AppointmentDetailsDto = await response.json();
    return data;
  } catch (error: any) {
    appInsights.trackException({ exception: error });
    // Rethrow any caught errors as a new error to be handled by the caller
    throw new Error(error.message || "An unexpected error occurred");
  }
};
