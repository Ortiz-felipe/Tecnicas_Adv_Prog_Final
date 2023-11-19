import {
  List,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import HomeIcon from "@mui/icons-material/Home";
import EventIcon from "@mui/icons-material/Event";
import DirectionsCarIcon from "@mui/icons-material/DirectionsCar";
import LogoutIcon from "@mui/icons-material/Logout";
import WorkIcon from "@mui/icons-material/Work";
import React from "react";
import { useNavigate } from "react-router-dom";
import { UserRole } from "../../types/dtos/Users/UserRole";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { logoutUser, selectUser } from "../../features/user/userSlice";

const SidePanel: React.FC = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const user = useAppSelector(selectUser);

  const handleNavigation = (path: string) => {
    navigate(path);
  };

  const handleLogout = () => {
    // Implement your logout logic here
    console.log("Logging out...");
    dispatch(logoutUser);
    // After logout, redirect to login page or landing page
    navigate("/login");
  };

  return (
    <List>
      {user && user.userRole === UserRole.RegularUser && (
        <>
          <ListItemButton onClick={() => handleNavigation("/home")}>
            <ListItemIcon>
              <HomeIcon />
            </ListItemIcon>
            <ListItemText primary="Inicio" />
          </ListItemButton>
          <ListItemButton onClick={() => handleNavigation("/appointments")}>
            <ListItemIcon>
              <EventIcon />
            </ListItemIcon>
            <ListItemText primary="Tus Citas" />
          </ListItemButton>
          <ListItemButton onClick={() => handleNavigation("/vehicles")}>
            <ListItemIcon>
              <DirectionsCarIcon />
            </ListItemIcon>
            <ListItemText primary="Tus Vehiculos" />
          </ListItemButton>
        </>
      )}
      {user && user.userRole === UserRole.Inspector && (
        <ListItemButton
          onClick={() => handleNavigation("/inspectorAppointments")}
        >
          <ListItemIcon>
            <WorkIcon />
          </ListItemIcon>
          <ListItemText primary="Turnos" />
        </ListItemButton>
      )}
      <ListItemButton onClick={handleLogout}>
        <ListItemIcon>
          <LogoutIcon />
        </ListItemIcon>
        <ListItemText primary="Cerrar Sesión" />
      </ListItemButton>
    </List>
  );
};

export default SidePanel;
