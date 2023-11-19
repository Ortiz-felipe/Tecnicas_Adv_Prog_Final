import React from 'react';
import { Typography, Paper, styled } from '@mui/material';
import { UserDto } from '../types/dtos/Users/UserDto';
import { VehicleDto } from '../types/dtos/Vehicles/VehicleDto';
import { formatDate } from '../helpers/dateHelpers';

const DetailPaper = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(2),
  marginBottom: theme.spacing(3),
  backgroundColor: '#f5f5f5', // A light grey background for minimalism
  boxShadow: 'none', // Remove shadow for a flatter design
  borderRadius: '8px', // Slightly rounded corners for a modern look
}));

const DetailHeader = styled(Typography)({
  fontWeight: 'bold',
  marginBottom: '0.35em', // Provide some spacing between the title and the details
});

const DetailText = styled(Typography)({
  lineHeight: '1.5', // Increase line-height for readability
  color: '#616161', // A softer color for text to reduce contrast
});

interface AppointmentDetailsProps {
  user: UserDto;
  vehicle: VehicleDto;
  appointmentDate: string;
}

const AppointmentDetails: React.FC<AppointmentDetailsProps> = ({ user, vehicle, appointmentDate }) => {
  return (
    <DetailPaper>
      <DetailHeader variant="h6">Detalles de la Cita</DetailHeader>
      <DetailText variant="subtitle1">Cliente: {user.fullName}</DetailText>
      <DetailText variant="subtitle1">{vehicle.brand} {vehicle.model} - {vehicle.licensePlate}</DetailText>
      <DetailText variant="subtitle1">Fecha: {formatDate(appointmentDate)}</DetailText>
    </DetailPaper>
  );
};

export default AppointmentDetails;
