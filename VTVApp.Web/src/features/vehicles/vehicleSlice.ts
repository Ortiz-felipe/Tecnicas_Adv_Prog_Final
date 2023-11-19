// src/features/vehicles/vehicleSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { VehicleDto } from '../../types/dtos/Vehicles/VehicleDto';

interface VehicleState {
  vehicles: VehicleDto[];
  favoriteVehicle: VehicleDto | null;
  loading: boolean;
  error: string | null;
}

const initialState: VehicleState = {
  vehicles: [],
  favoriteVehicle: null,
  loading: false,
  error: null,
};

const vehicleSlice = createSlice({
  name: 'vehicles',
  initialState,
  reducers: {
    startLoading: (state) => {
      state.loading = true;
    },
    setVehicles: (state, action: PayloadAction<VehicleDto[]>) => {
      state.vehicles = action.payload;
      state.loading = false;
    },
    setFavoriteVehicle: (state, action: PayloadAction<VehicleDto>) => {
      state.favoriteVehicle = action.payload;
      state.loading = false;
    },
    setError: (state, action: PayloadAction<string>) => {
      state.error = action.payload;
      state.loading = false;
    },
    clearVehicles: (state) => {
      state.vehicles = [];
      state.favoriteVehicle = null;
      state.loading = false;
      state.error = null;
    },
    // Add any other reducers for additional actions
  },
});

// Export the actions
export const { 
  startLoading, 
  setVehicles, 
  setFavoriteVehicle, 
  setError, 
  clearVehicles 
} = vehicleSlice.actions;

// Selectors
export const selectVehicles = (state: RootState) => state.vehicles.vehicles;
export const selectFavoriteVehicle = (state: RootState) => state.vehicles.favoriteVehicle;
export const selectVehiclesLoading = (state: RootState) => state.vehicles.loading;
export const selectVehiclesError = (state: RootState) => state.vehicles.error;

export default vehicleSlice.reducer;
