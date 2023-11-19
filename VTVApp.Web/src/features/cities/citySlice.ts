// src/features/city/citySlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { CityDetailsDto } from '../../types/dtos/Cities/CityDetailsDto';

interface CityState {
  cities: CityDetailsDto[];
  loading: boolean;
  error: string | null;
}

const initialState: CityState = {
  cities: [],
  loading: false,
  error: null,
};

const citySlice = createSlice({
  name: 'cities',
  initialState,
  reducers: {
    startCityLoading: (state) => {
      state.loading = true;
      state.error = null;
    },
    citiesReceived: (state, action: PayloadAction<CityDetailsDto[]>) => {
      state.cities = action.payload;
      state.loading = false;
    },
    citiesFetchFailed: (state, action: PayloadAction<string>) => {
      state.error = action.payload;
      state.loading = false;
    },
    // Add any other reducers you might need for city operations
  },
});

// Export the actions
export const { startCityLoading, citiesReceived, citiesFetchFailed } = citySlice.actions;

// Selectors
export const selectCities = (state: RootState) => state.cities.cities;
export const selectCityLoading = (state: RootState) => state.cities.loading;
export const selectCityError = (state: RootState) => state.cities.error;

export default citySlice.reducer;
