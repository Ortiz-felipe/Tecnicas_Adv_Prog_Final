// src/features/user/userSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { UserDto } from '../../types/dtos/Users/UserDto';

interface UserState {
  isAuthenticated: boolean;
  token: string | null;
  user: UserDto | null;
  errorMessage: string | null;
}

const initialState: UserState = {
  isAuthenticated: false,
  token: null,
  user: null,
  errorMessage: null,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<UserDto>) => {
      state.isAuthenticated = true;
      state.user = action.payload;
      state.errorMessage = null;
    },
    setToken: (state, action: PayloadAction<string>) => {
      state.token = action.payload;
    },
    setAuthenticationError: (state, action: PayloadAction<string>) => {
      state.isAuthenticated = false;
      state.token = null;
      state.user = null;
      state.errorMessage = action.payload;
    },
    logoutUser: (state) => {
      state.isAuthenticated = false;
      state.token = null;
      state.user = null;
      state.errorMessage = null;
    },
  },
});

// Export actions
export const { setUser, setToken, setAuthenticationError, logoutUser } = userSlice.actions;

// Selectors
export const selectIsAuthenticated = (state: RootState) => state.user.isAuthenticated;
export const selectUser = (state: RootState) => state.user.user;
export const selectUserToken = (state: RootState) => state.user.token;
export const selectErrorMessage = (state: RootState) => state.user.errorMessage;

// Export reducer
export default userSlice.reducer;
