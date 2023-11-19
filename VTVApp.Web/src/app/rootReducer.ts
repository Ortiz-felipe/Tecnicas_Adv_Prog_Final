// src/app/rootReducer.ts
import { combineReducers } from '@reduxjs/toolkit';
import userReducer from '../features/user/userSlice';
import provinceReducer from '../features/provinces/provinceSlice';
import cityReducer from '../features/cities/citySlice';
import appointmentReducer from '../features/appointments/appointmentSlice';
import inspectionsReducer from '../features/inspections/inspectionSlice';
import vehicleReducer from '../features/vehicles/vehicleSlice';

const rootReducer = combineReducers({
  user: userReducer,
  provinces: provinceReducer,
  cities: cityReducer,
  appointments: appointmentReducer,
  inspections: inspectionsReducer,
  vehicles: vehicleReducer
  // Add other feature reducers here as they are developed
});

export default rootReducer;
