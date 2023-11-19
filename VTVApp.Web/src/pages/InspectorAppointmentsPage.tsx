import React, { useEffect, useState } from "react";
import {
  Typography,
  IconButton,
  CircularProgress,
  styled,
} from "@mui/material";
import PlayCircleOutlineIcon from "@mui/icons-material/PlayCircleOutline";
import TableComponent from "../components/Table";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import {
  selectAppointmentDetails,
  selectAppointmentList,
  setAppointmentDetails,
  setAppointmentList,
  setError,
} from "../features/appointments/appointmentSlice";
import { getAllAppointments, getAppointmentById } from "../api/appointmentAPI";
import { formatDate } from "../helpers/dateHelpers";
import { CreateInspectionDto } from "../types/dtos/Inspections/CreateInspectionDto";
import moment from "moment";
import { INSPECTION_CHECKPOINTS } from "../types/constants/InspectionCheckpoints";
import { createInspection, saveInspectionResults } from "../api/inspectionAPI";
import {
  selectInspectionDetails,
  setInspectionDetails,
} from "../features/inspections/inspectionSlice";
import { UpdateInspectionDto } from "../types/dtos/Inspections/UpdateInspectionDto";
import Modal from "../components/Modal";
import AppointmentDetails from "../components/AppointmentDetails";
import CheckpointStepper from "../components/CheckpointStepper";
import RefreshIcon from "@mui/icons-material/Refresh";
import { getAppointmentStatusDescription } from "../helpers/appointmentStatusHelper";
import { Theme } from '@mui/material/styles';
import { CheckpointListDto } from "../types/dtos/Checkpoints/CheckpointListDto";

const SpinnerContainer = styled("div")(() => ({
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
  height: "100vh",
}));

// const CurrentDayContainer = styled(Typography)(({ theme }: { theme: Theme }) => ({
//   textAlign: "center",
//   margin: theme.spacing(2, 0),
// }));

const CenteredMessage = styled(Typography)(({ theme }: { theme: Theme }) => ({
  marginTop: theme.spacing(10),
  textAlign: "center",
}));

const InspectorAppointmentsPage: React.FC = () => {
  const dispatch = useAppDispatch();
  const appointments = useAppSelector(selectAppointmentList);
  const appointmentDetails = useAppSelector(selectAppointmentDetails);
  const inspectionDetails = useAppSelector(selectInspectionDetails);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [loading, setLoading] = useState(false);
  const [isLoadingDetails, setIsLoadingDetails] = useState(false);
  const userLocale = navigator.language;

  const todayFormatted = new Date().toLocaleDateString(userLocale, {
    day: "numeric",
    month: "long",
    year: "numeric",
    weekday: "long",
  });

  useEffect(() => {
    fetchAppointments();
  }, [dispatch]);

  const handleStartAppointment = (appointmentId: string) => {
    setIsModalOpen(true);
    fetchAppointmentDetails(appointmentId);
  };

  const fetchAppointments = () => {
    setLoading(true);
    getAllAppointments()
      .then((appointmentsData) => {
        dispatch(setAppointmentList(appointmentsData));
      })
      .catch((error) => {
        dispatch(setError(error.message));
      })
      .finally(() => {
        setLoading(false);
      });
  };

  const fetchAppointmentDetails = (appointmentId: string) => {
    setIsLoadingDetails(true);
    getAppointmentById(appointmentId)
      .then((details) => {
        dispatch(setAppointmentDetails(details));
        // Create Inspection & Checkpoints
        const inspectionData: CreateInspectionDto = {
          appointmentId: appointmentId,
          result: 0,
          vehicleId: details.vehicle.id,
          inspectionDate: moment().utc().toISOString(),
          checkpoints: INSPECTION_CHECKPOINTS.map((checkpoint) => ({
            name: checkpoint.checkpointName,
            score: 0,
            comment: "",
          })) as CheckpointListDto[],
        };
        return createInspection(inspectionData, appointmentId);
      })
      .then((inspectionResult) => {
        if (inspectionResult.success) {
          dispatch(setInspectionDetails(inspectionResult.inspectionDetails));
        } else {
          dispatch(setError(inspectionResult.message));
        }
      })
      .catch((error) => {
        dispatch(setError(error.message));
      })
      .finally(() => {
        setIsLoadingDetails(false);
      });
  };

  const handleCompleteInspection = async (
    updateInspectionData: UpdateInspectionDto
  ) => {
    setIsLoadingDetails(true);
    try {
      const inspectionData: UpdateInspectionDto = {
        ...updateInspectionData,
        id: inspectionDetails!.id,
        appointmentId: appointmentDetails!.id,
      };
      const saveResultsResponse = await saveInspectionResults(
        inspectionData,
        inspectionDetails!.id
      );
      if (saveResultsResponse.success) {
        dispatch(setInspectionDetails(saveResultsResponse.inspectionDetails));
        setIsModalOpen(false);
      } else {
        throw new Error(saveResultsResponse.message);
      }
    } catch (error: any) {
      dispatch(setError(error.message));
    } finally {
      setIsLoadingDetails(false);
    }
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  const columns = [
    { id: "userFullName", label: "Nombre Completo", minWidth: 170 },
    { id: "vehicleLicensePlate", label: "Placa del Vehículo", minWidth: 170 },
    {
      id: "appointmentDate",
      label: "Fecha de la Cita",
      minWidth: 170,
      format: formatDate,
    },
    {
      id: "appointmentStatus",
      label: "Estado de la Cita",
      minWidth: 170,
      format: getAppointmentStatusDescription,
    },
  ];

  const actions = [
    {
      icon: <PlayCircleOutlineIcon />,
      tooltip: "Iniciar Cita",
      onClick: (item: any) => handleStartAppointment(item.id),
    },
  ];

  return (
    <>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <Typography variant="h4" gutterBottom>
          Inspecciones para el día: {todayFormatted}
        </Typography>
        <IconButton
          aria-label="refresh"
          onClick={fetchAppointments}
          disabled={loading}
        >
          <RefreshIcon />
        </IconButton>
      </div>
      {loading ? (
        <SpinnerContainer>
          <CircularProgress />
        </SpinnerContainer>
      ) : appointments.length > 0 ? (
        <TableComponent
          columns={columns}
          data={appointments}
          actions={actions}
        />
      ) : (
        <CenteredMessage variant="subtitle1">
          No hay citas programadas para hoy.
        </CenteredMessage>
      )}
      {isModalOpen && (
        <Modal
          open={isModalOpen}
          onClose={handleCloseModal}
          title="Detalles de la Inspección"
        >
          {isLoadingDetails ? (
            <CircularProgress />
          ) : (
            <>
              <AppointmentDetails
                user={appointmentDetails!.user}
                vehicle={appointmentDetails!.vehicle}
                appointmentDate={appointmentDetails!.appointmentDate}
              />
              <CheckpointStepper
                onComplete={handleCompleteInspection}
                inspectionId={inspectionDetails!.id}
                appointmentId={appointmentDetails!.id}
                checkpoints={inspectionDetails!.checkpoints}
              />
            </>
          )}
        </Modal>
      )}
    </>
  );
};

export default InspectorAppointmentsPage;
