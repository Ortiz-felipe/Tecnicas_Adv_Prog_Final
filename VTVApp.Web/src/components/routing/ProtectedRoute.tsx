// src/components/ProtectedRoute.tsx
import React from 'react';
import { Navigate, useLocation, Outlet } from 'react-router-dom';
import { useAppSelector } from '../../app/hooks';
import { selectIsAuthenticated } from '../../features/user/userSlice';

const ProtectedRoute: React.FC = () => {
  const isAuthenticated = useAppSelector(selectIsAuthenticated);
  const location = useLocation();

  if (!isAuthenticated) {
    // Redirect to the /login page and save the current location
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // Render children routes if authenticated
  return <Outlet />;
};

export default ProtectedRoute;
