// src/features/inspections/inspectionSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { InspectionListDto } from '../../types/dtos/Inspections/InspectionListDto';
import { InspectionDetailsDto } from '../../types/dtos/Inspections/InspectionDetailsDto';

interface InspectionState {
  inspectionList: InspectionListDto[];
  inspectionDetails: InspectionDetailsDto | null;
  loading: boolean;
  error: string | null;
}

const initialState: InspectionState = {
  inspectionList: [],
  inspectionDetails: null,
  loading: false,
  error: null,
};

const inspectionSlice = createSlice({
  name: 'inspections',
  initialState,
  reducers: {
    startLoading: (state) => {
      state.loading = true;
    },
    setInspectionList: (state, action: PayloadAction<InspectionListDto[]>) => {
      state.inspectionList = action.payload;
      state.loading = false;
    },
    setInspectionDetails: (state, action: PayloadAction<InspectionDetailsDto>) => {
      state.inspectionDetails = action.payload;
      state.loading = false;
    },
    setError: (state, action: PayloadAction<string>) => {
      state.error = action.payload;
      state.loading = false;
    },
    clearInspections: (state) => {
      state.inspectionDetails = null;
      state.inspectionList = [];
      state.loading = false;
      state.error = null;
    },
    // Add any other reducers for additional actions
  },
});

// Export the actions
export const { 
  startLoading, 
  setInspectionList, 
  setInspectionDetails, 
  setError, 
  clearInspections 
} = inspectionSlice.actions;

// Selectors
export const selectInspectionList = (state: RootState) => state.inspections.inspectionList;
export const selectInspectionDetails = (state: RootState) => state.inspections.inspectionDetails;
export const selectInspectionsLoading = (state: RootState) => state.inspections.loading;
export const selectInspectionsError = (state: RootState) => state.inspections.error;

export default inspectionSlice.reducer;
