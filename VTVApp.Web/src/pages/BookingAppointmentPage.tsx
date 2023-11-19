import React, { useState, useEffect } from "react";
import {
  Button,
  Container,
  Typography,
  MenuItem,
  FormControl,
  InputLabel,
  Select,
  Paper,
  Backdrop,
  CircularProgress,
  Alert,
  SelectChangeEvent,
} from "@mui/material";
import { AdapterMoment } from "@mui/x-date-pickers/AdapterMoment";
import { LocalizationProvider, DesktopDatePicker } from "@mui/x-date-pickers";
import { useNavigate } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { selectVehicles, setVehicles } from "../features/vehicles/vehicleSlice";
import moment from "moment";
import styled from "@emotion/styled";
import { getAllVehiclesForUserId } from "../api/vehicleAPI";
import { selectUser } from "../features/user/userSlice";
import {
  createNewAppointment,
  getAvailableTimeSlotsForSelectedDate,
} from "../api/appointmentAPI";
import { VehicleDto } from "../types/dtos/Vehicles/VehicleDto";
import { CreateAppointmentDto } from "../types/dtos/Appointments/CreateAppointmentDto";
import { Theme } from "@mui/material/styles";

const StyledContainer = styled(Container)<{ theme?: Theme }>(({ theme }) => ({
  paddingTop: theme.spacing(6),
  paddingBottom: theme.spacing(6),
}));

const StyledPaper = styled(Paper)<{ theme?: Theme }>(({ theme }) => ({
  padding: theme.spacing(4),
  display: "flex",
  flexDirection: "column",
  alignItems: "flex-start", // Align items to the left
}));

const TitleTypography = styled(Typography)({
  alignSelf: "flex-start", // Align title to the left
  marginBottom: "1rem", // Add some margin below the title
});

const FormSection = styled("div")({
  width: "100%", // Take the full width of the parent
  display: "flex",
  flexDirection: "row", // Align children in a row
  justifyContent: "space-between", // Distribute space between children
  alignItems: "center", // Center children vertically
  marginBottom: "1rem", // Add some margin below the section
});

const DatePickerWrapper = styled("div")({
  flexBasis: "50%", // Take up half of the container width
});

const VehicleSelectWrapper = styled("div")({
  flexBasis: "50%", // Take up the remaining half
});

const ButtonSection = styled("div")({
  display: "flex",
  justifyContent: "space-between", // Distribute space between buttons
  width: "100%", // Take the full width of the parent
});

const StyledButton = styled(Button)<{ theme?: Theme }>(({ theme }) => ({
  marginTop: theme.spacing(2), // Add margin above the buttons
}));

// Use this component to render the DatePicker and Select components side by side
// const FormRow = styled(Grid)(({ theme }: { theme: Theme }) => ({
//   [theme.breakpoints.down("sm")]: {
//     flexDirection: "column",
//   },
// }));

const BookAppointmentPage: React.FC = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const userDetails = useAppSelector(selectUser);
  const vehicles = useAppSelector(selectVehicles); // Retrieve vehicles from Redux store
  const [selectedDate, setSelectedDate] = useState<moment.Moment | null>(
    moment()
  );
  const [selectedTime, setSelectedTime] = useState("");
  const [selectedVehicleId, setSelectedVehicleId] = useState(""); // This will store the ID
  const [selectedVehicle, setSelectedVehicle] = useState<VehicleDto | null>(
    null
  ); // This will store the full details

  const [availableTimeSlots, setAvailableTimeSlots] = useState<string[]>();
  const [isCreatingAppointment, setIsCreatingAppointment] = useState(false);
  const [alertInfo, setAlertInfo] = useState({ show: false, message: "" });

  useEffect(() => {
    const fetchAvailableVehiclesForUser = async () => {
      if (vehicles.length === 0 && userDetails) {
        const availableVehicles = await getAllVehiclesForUserId(userDetails.id);
        dispatch(setVehicles(availableVehicles));
      }
    };

    fetchAvailableVehiclesForUser();
  }, [vehicles]);

  useEffect(() => {
    const fetchAndSetAvailableSlots = async () => {
      if (selectedDate) {
        try {
          const slots = await getAvailableTimeSlotsForSelectedDate(
            selectedDate.format("YYYY-MM-DD").toString()
          );
          setAvailableTimeSlots(slots.availableSlots);
          setSelectedTime(""); // Reset the selected time when date changes
        } catch (error) {
          console.error("Failed to fetch available time slots:", error);
          // Handle the error state appropriately
        }
      }
    };

    fetchAndSetAvailableSlots();
  }, [selectedDate]);

  const handleDateChange = (date: moment.Moment | null) => {
    setSelectedDate(date);
  };

  const handleVehicleChange = (
    event: SelectChangeEvent<string>
  ) => {
    const newVehicleId = event.target.value as string;
    setSelectedVehicleId(newVehicleId);
  
    const vehicleDetails = vehicles.find(
      (vehicle) => vehicle.id === newVehicleId
    );
    setSelectedVehicle(vehicleDetails || null);
  };
  

  const confirmAppointment = async () => {
    // Reset the alert state
    setAlertInfo({ show: false, message: "" });

    if (!selectedDate || !selectedTime || !selectedVehicle) {
      setAlertInfo({
        show: true,
        message: "Por favor, seleccione fecha, hora y vehículo para su cita.",
      });
      return;
    }

    try {
      setIsCreatingAppointment(true); // Start loading

      const appointmentDateTime = moment(selectedDate)
        .hour(moment(selectedTime, "HH:mm").hour()) // Make sure to parse the selectedTime
        .minute(moment(selectedTime, "HH:mm").minute())
        .second(0) // Reset seconds to 0 for consistency
        .millisecond(0) // Reset milliseconds to 0 for consistency
        .utc() // Convert to UTC
        .toISOString(); // Convert to ISO string format

      // Prepare the appointment data
      const appointmentData: CreateAppointmentDto = {
        appointmentDate: appointmentDateTime,
        userId: userDetails!.id, // Assuming you have the user's ID in userDetails
        vehicleId: selectedVehicle.id,
      };

      // Call the API to create a new appointment
      const appointmentDetails = await createNewAppointment(appointmentData);
      console.log("Appointment created:", appointmentDetails);

      navigate("/appointments"); // Navigate to the appointments page on success
    } catch (error) {
      console.error("Failed to create appointment:", error);
      alert("Failed to create appointment. Please try again."); // Show error message
    } finally {
      setIsCreatingAppointment(false); // End loading regardless of outcome
    }
  };

  const cancelAppointment = () => {
    navigate("/appointments"); // Navigate back to the appointments page
  };

  return (
    <StyledContainer maxWidth="md">
      {alertInfo.show && (
        <Alert
          severity="warning"
          onClose={() => setAlertInfo({ show: false, message: "" })}
        >
          {alertInfo.message}
        </Alert>
      )}
      <StyledPaper>
        <TitleTypography variant="h4" gutterBottom>
          Reserva una Nueva Cita
        </TitleTypography>
        <LocalizationProvider dateAdapter={AdapterMoment}>
          <FormSection>
            <DatePickerWrapper>
              <DesktopDatePicker
                label="Fecha de la Cita"
                format="DD/MM/yyyy"
                value={selectedDate}
                onChange={handleDateChange}
                disablePast
                // renderInput={(params: TextFieldProps) => <TextField {...params} fullWidth />}
              />
            </DatePickerWrapper>
            <VehicleSelectWrapper>
              <FormControl fullWidth>
                <InputLabel id="appointment-time-label">
                  Hora de la Cita
                </InputLabel>
                <Select
                  labelId="appointment-time-label"
                  id="appointment-time-select"
                  value={selectedTime}
                  label="Hora de la Cita"
                  onChange={(e) => setSelectedTime(e.target.value as string)}
                >
                  {availableTimeSlots && availableTimeSlots?.length > 0 ? (
                    availableTimeSlots?.map((timeSlot) => (
                      <MenuItem key={timeSlot} value={timeSlot}>
                        {moment(timeSlot).format("LT")}
                      </MenuItem>
                    ))
                  ) : (
                    <MenuItem disabled>
                      Selecciona una fecha para ver los horarios disponibles
                    </MenuItem>
                  )}
                </Select>
              </FormControl>
            </VehicleSelectWrapper>
          </FormSection>
          <FormControl fullWidth>
            <InputLabel id="vehicle-select-label">Vehículo</InputLabel>
            <Select
              labelId="vehicle-select-label"
              id="vehicle-select"
              value={selectedVehicleId}
              label="Vehículo"
              onChange={handleVehicleChange}
            >
              {vehicles.map((vehicle) => (
                <MenuItem key={vehicle.id} value={vehicle.id}>
                  {`${vehicle.brand} ${vehicle.model} - ${vehicle.licensePlate}`}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <ButtonSection>
            <StyledButton
              variant="contained"
              color="primary"
              onClick={confirmAppointment}
              disabled={isCreatingAppointment}
            >
              Confirmar
            </StyledButton>
            <StyledButton
              variant="outlined"
              color="secondary"
              onClick={cancelAppointment}
            >
              Cancelar
            </StyledButton>
          </ButtonSection>
        </LocalizationProvider>
      </StyledPaper>
      {isCreatingAppointment && (
        <Backdrop open={true} style={{ color: "#fff", zIndex: 1 }}>
          <CircularProgress color="inherit" />
        </Backdrop>
      )}
    </StyledContainer>
  );
};

export default BookAppointmentPage;
