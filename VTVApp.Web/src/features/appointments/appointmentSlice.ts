// src/features/appointments/appointmentSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { AppointmentDetailsDto } from '../../types/dtos/Appointments/AppointmentDetailsDto';
import { AppointmentListDto } from '../../types/dtos/Appointments/AppointmentListDto';


interface AppointmentState {
  appointmentDetails: AppointmentDetailsDto | null;
  appointmentList: AppointmentListDto[];
  loading: boolean;
  error: string | null;
}

const initialState: AppointmentState = {
  appointmentDetails: null,
  appointmentList: [],
  loading: false,
  error: null,
};

export const appointmentSlice = createSlice({
  name: 'appointments',
  initialState,
  reducers: {
    startLoading: (state) => {
      state.loading = true;
      state.error = null;
    },
    setAppointmentDetails: (state, action: PayloadAction<AppointmentDetailsDto>) => {
      state.appointmentDetails = action.payload;
      state.loading = false;
    },
    setAppointmentList: (state, action: PayloadAction<AppointmentListDto[]>) => {
      state.appointmentList = action.payload;
      state.loading = false;
    },
    setError: (state, action: PayloadAction<string>) => {
      state.error = action.payload;
      state.loading = false;
    },
    clearAppointments: (state) => {
      state.appointmentDetails = null;
      state.appointmentList = [];
      state.error = null;
      // Reset loading if needed
      state.loading = false;
    },
    // Add any other reducers you might need for appointment operations
  },
});

// Export the actions
export const { startLoading, setAppointmentDetails, setAppointmentList, setError, clearAppointments } = appointmentSlice.actions;

// Selectors
export const selectAppointmentDetails = (state: RootState) => state.appointments.appointmentDetails;
export const selectAppointmentList = (state: RootState) => state.appointments.appointmentList;
export const selectAppointmentsLoading = (state: RootState) => state.appointments.loading;
export const selectAppointmentsError = (state: RootState) => state.appointments.error;

export default appointmentSlice.reducer;
