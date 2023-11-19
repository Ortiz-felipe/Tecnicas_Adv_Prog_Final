import React, { useEffect } from "react";
import { Button, Typography } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import SearchIcon from "@mui/icons-material/Search";
import styled from "@emotion/styled";
import TableComponent from "../components/Table";
import { formatDate } from "../helpers/dateHelpers";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { selectUser } from "../features/user/userSlice";
import {
  selectAppointmentList,
  setAppointmentList,
  setError,
  startLoading,
} from "../features/appointments/appointmentSlice";
import { getAllAppointmentsForUser } from "../api/appointmentAPI";
import { useNavigate } from "react-router-dom";
import { getAppointmentStatusDescription } from "../helpers/appointmentStatusHelper";

const AppointmentsPageContainer = styled.div`
  width: 100%; // Take the full width of the content area
  padding: 1rem;
  display: flex;
  flex-direction: column;
  align-items: center; // Center children horizontally
  justify-content: center; // Center children vertically when there is no table
`;

const HeaderSection = styled.div`
  display: flex;
  width: 100%; // Ensure it spans the full width of AppointmentsPageContainer
  justify-content: space-between; // Space items as requested
  align-items: center;
  margin-bottom: 2rem; // Provide some space between the header and the content
`;

const NoContentMessage = styled(Typography)`
  text-align: center;
  width: 100%; // Ensure it spans the full width of its container
  margin-top: 2rem
`;


const columns = [
  { id: "appointmentDate", label: "Fecha de la Cita", minWidth: 170, format: formatDate },
  { id: "userFullName", label: "Nombre del Propietario", minWidth: 100 },
  { id: "vehicleLicensePlate", label: "Número de Patente", minWidth: 170 },
  { id: "appointmentStatus", label: "Estado de la Cita", minWidth: 100, format: getAppointmentStatusDescription },
];

const actions = [
  {
    icon: <SearchIcon />,
    tooltip: "Ver detalles",
    onClick: () => {
      // Logic to open the modal with appointment details
      return;
    },
  },
  // Add other actions here as needed
];

const AppointmentsPage: React.FC = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const userDetails = useAppSelector(selectUser);
  const appointments = useAppSelector(selectAppointmentList);

  useEffect(() => {
    const fetchAppointments = async () => {
      if (userDetails) {
        dispatch(startLoading());
        try {
          const appointmentsList = await getAllAppointmentsForUser(
            userDetails.id
          );
          dispatch(setAppointmentList(appointmentsList));
        } catch (error: any) {
          dispatch(setError(error.message));
        }
      }
    };

    fetchAppointments();
  }, [dispatch, userDetails]);

  const handleBookAppointment = () => {
    navigate('newAppointment');
  };

  // const handleViewDetails = (appointment: AppointmentListDto) => {
  //   // Logic to open the modal with appointment details
  // };

  return (
    <AppointmentsPageContainer
      style={{
        justifyContent: appointments.length > 0 ? "flex-start" : "center",
      }}
    >
      <HeaderSection>
        <Typography variant="h4">Tus Citas Programadas</Typography>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={handleBookAppointment}
        >
          Agendar Nueva Cita
        </Button>
      </HeaderSection>
      {appointments.length > 0 ? (
        <TableComponent
          columns={columns}
          data={appointments}
          actions={actions}
        />
      ) : (
        <NoContentMessage variant="h6">
          No tienes citas programadas.
        </NoContentMessage>
      )}
    </AppointmentsPageContainer>
  );
};

export default AppointmentsPage;
