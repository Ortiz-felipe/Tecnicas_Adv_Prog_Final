// src/features/province/provinceSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { ProvinceDto } from '../../types/dtos/Provinces/ProvinceDto';

interface ProvinceState {
  provinces: ProvinceDto[];
  loading: boolean;
  error: string | null;
}

const initialState: ProvinceState = {
  provinces: [],
  loading: false,
  error: null,
};

const provinceSlice = createSlice({
  name: 'provinces',
  initialState,
  reducers: {
    startLoading: (state) => {
      state.loading = true;
      state.error = null;
    },
    provincesReceived: (state, action: PayloadAction<ProvinceDto[]>) => {
      state.provinces = action.payload;
      state.loading = false;
    },
    provincesFetchFailed: (state, action: PayloadAction<string>) => {
      state.error = action.payload;
      state.loading = false;
    },
    // You can add more reducers as needed
  },
});

// Export the actions
export const { startLoading, provincesReceived, provincesFetchFailed } = provinceSlice.actions;

// Selectors
export const selectProvinces = (state: RootState) => state.provinces.provinces;
export const selectProvinceLoading = (state: RootState) => state.provinces.loading;
export const selectProvinceError = (state: RootState) => state.provinces.error;

export default provinceSlice.reducer;
