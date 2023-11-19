import React, { useState, useEffect } from "react";
import { Grid, Typography, Skeleton } from "@mui/material";

import CardComponent from "../components/Card";
import styled from "@emotion/styled";
import { VehicleDto } from "../types/dtos/Vehicles/VehicleDto";
import { selectUser } from "../features/user/userSlice";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { UserDto } from "../types/dtos/Users/UserDto";
import { getFavoriteVehicleForUserId } from "../api/vehicleAPI";
import { getLatestAppointment } from "../api/appointmentAPI";
import { getLatestVehicleInspection } from "../api/inspectionAPI";
import {
  selectFavoriteVehicle,
  setFavoriteVehicle,
} from "../features/vehicles/vehicleSlice";
import {
  selectAppointmentDetails,
  setAppointmentDetails,
} from "../features/appointments/appointmentSlice";
import {
  selectInspectionDetails,
  setInspectionDetails,
} from "../features/inspections/inspectionSlice";
import { AppointmentDetailsDto } from "../types/dtos/Appointments/AppointmentDetailsDto";
import { InspectionDetailsDto } from "../types/dtos/Inspections/InspectionDetailsDto";
import moment from "moment";
import { formatDate } from "../helpers/dateHelpers";
import { getInspectionStatusDescription } from "../helpers/inspectionStatusHelper";

// Define styled components for layout
const StyledGrid = styled(Grid)(({ theme }) => ({
  padding: theme.spacing(3), // Increased padding for more space around the grid
  alignItems: 'center', // Center align items
}));

const ContentTypography = styled(Typography)(({ theme }) => ({
  fontSize: '1rem', // Ensure consistent font-size
  color: theme.palette.text.primary, // Use primary text color for better contrast
  lineHeight: '1.5', // Increase line-height for readability
}));

// Adjust cardStyle for a minimalistic design
const cardStyle = {
  boxShadow: '0 0.25rem 0.75rem rgba(0,0,0,0.05)', // Softer shadow
  borderRadius: '0.5rem', // Rounded corners in REMs
  padding: '1.5rem', // Padding in REMs
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'center', // Center content vertically
  alignItems: 'center', // Center content horizontally
  height: '15rem', // Fixed height in REMs
  width: '100%', // Full width
  backgroundColor: '#fff', // White background
  marginBottom: '2rem', // Margin bottom in REMs
  '&:last-child': {
    marginBottom: 0,
  },
};

// Apply similar styles to Skeleton for consistency
const skeletonStyle = {
  ...cardStyle,
};

const HomePage: React.FC = () => {
  const dispatch = useAppDispatch();
  const userDetails = useAppSelector(selectUser);
  const favoriteVehicle = useAppSelector(selectFavoriteVehicle);
  const latestAppointment = useAppSelector(selectAppointmentDetails);
  const latestInspection = useAppSelector(selectInspectionDetails);
  const [isDataLoading, setIsDataLoading] = useState(true);
  const [isVehicleInspectionLoading, setIsVehicleInspectionLoading] =
    useState(true);

  useEffect(() => {
    const loadData = async () => {
      if (userDetails) {
        try {
          setIsDataLoading(true);
          const [favoriteVehicleData, latestAppointmentData] =
            await Promise.all([
              getFavoriteVehicleForUserId(userDetails.id),
              getLatestAppointment(userDetails.id),
            ]);
          dispatch(setFavoriteVehicle(favoriteVehicleData));
          dispatch(setAppointmentDetails(latestAppointmentData));
        } catch (error) {
          console.error(error);
        } finally {
          setIsDataLoading(false);
        }
      }
    };

    loadData();
  }, []);

  useEffect(() => {
    const loadVehicleInspectionData = async (vehicleId: string) => {
      try {
        setIsVehicleInspectionLoading(true);
        const latestInspectionData = await getLatestVehicleInspection(
          vehicleId
        );
        dispatch(setInspectionDetails(latestInspectionData));
      } catch (error) {
        console.error(error);
      } finally {
        setIsVehicleInspectionLoading(false);
      }
    };
    if (favoriteVehicle) {
      loadVehicleInspectionData(favoriteVehicle.id);
    } else {
      setIsVehicleInspectionLoading(false);
    }
  }, [favoriteVehicle, dispatch]);

  return (
    <StyledGrid container spacing={2}>
      <StyledGrid item xs={12} md={6}>
        <CardComponent
          title="Informacion personal"
          content={userDetails}
          style={cardStyle}
          renderContent={(userContent: UserDto) => (
            <>
              <ContentTypography variant="body1">
                Nombre: {userContent.fullName}
              </ContentTypography>
              <ContentTypography variant="body1">
                Email: {userContent.email}
              </ContentTypography>
              <ContentTypography variant="body1">
                Ciudad: {userContent.cityName}
              </ContentTypography>
              <ContentTypography variant="body1">
                Provincia: {userContent.provinceName}
              </ContentTypography>
            </>
          )}
        />
        {isDataLoading ? (
          <Skeleton
            variant="rectangular"
            height={118}
            animation="wave"
            sx={skeletonStyle}
          />
        ) : latestAppointment ? (
          <CardComponent
            title="Próxima Cita"
            content={latestAppointment}
            style={cardStyle}
            renderContent={(appointmentContent: AppointmentDetailsDto) => (
              <>
                <ContentTypography variant="body1">
                  Fecha: {formatDate(appointmentContent.appointmentDate)}
                </ContentTypography>
                <ContentTypography variant="body1">
                  Vehículo: {appointmentContent.vehicle.licensePlate}
                </ContentTypography>
                {/* Add more details as needed */}
              </>
            )}
          />
        ) : (
          <Typography variant="subtitle1" style={cardStyle}>
            No tiene citas próximas.
          </Typography>
        )}
      </StyledGrid>
      <StyledGrid item xs={12} md={6}>
        {isDataLoading ? (
          <Skeleton
            variant="rectangular"
            height={118}
            animation="wave"
            sx={skeletonStyle}
          />
        ) : favoriteVehicle ? (
          <CardComponent
            title="Vehículo Favorito"
            content={favoriteVehicle}
            style={cardStyle}
            renderContent={(vehicleContent: VehicleDto) => (
              <>
                <ContentTypography variant="body1">
                  Matrícula: {vehicleContent.licensePlate}
                </ContentTypography>
                <ContentTypography variant="body1">
                  Marca: {vehicleContent.brand}
                </ContentTypography>
                <ContentTypography variant="body1">
                  Modelo: {vehicleContent.model}
                </ContentTypography>
                <ContentTypography variant="body1">
                  Color: {vehicleContent.color}
                </ContentTypography>
              </>
            )}
          />
        ) : (
          <Typography variant="subtitle1" style={cardStyle}>
            No tiene un vehículo favorito seleccionado.
          </Typography>
        )}
        {isVehicleInspectionLoading ? (
          <Skeleton
            variant="rectangular"
            height={118}
            animation="wave"
            sx={skeletonStyle}
          />
        ) : latestInspection ? (
          <CardComponent
            title="Última Inspección"
            content={latestInspection}
            renderContent={(inspectionContent: InspectionDetailsDto) => (
              <>
                <ContentTypography variant="body1">
                  Fecha: {formatDate(inspectionContent.inspectionDate)}
                </ContentTypography>
                <ContentTypography variant="body1">
                  Estado: {getInspectionStatusDescription(inspectionContent.status)}
                </ContentTypography>
              </>
            )}
          />
        ) : (
          <Typography variant="subtitle1" style={cardStyle}>
            No tiene inspecciones recientes.
          </Typography>
        )}
      </StyledGrid>
    </StyledGrid>
  );
};

export default HomePage;
